using System;
using System.Windows.Forms;
using System.Drawing;

namespace Maze
{
    class Labirint
    {
        //стартовая позиция для персонажа
        public int smileX { get; set; }
        public int smileY { get; set; }

        public int height { get; } // высота лабиринта (количество строк)
        public int width { get; } // ширина лабиринта (количество столбцов в каждой строке)

        public MazeObject[,] maze; //массив мазеобджектов
        public PictureBox[,] images;//массив элементов управления, на каждый из которых ложатся
                                    //(отображаются) картинки (есть в ToolBox)

        public static Random r = new Random();
        public Form parent;//указывает, кто родительская форма (взаимодействие с типом Формы)

        public int smileHealth { get; set; }
        public int medalCount { get; set; }

        public int smileEnergy { get; set; }
        public int smileStep { get; set; }
        public Labirint(Form parent, int width, int height)
        {
            this.width = width;
            this.height = height;
            this.parent = parent;
            medalCount = 0;
            smileHealth = 100;
            smileEnergy = 100;
            maze = new MazeObject[height, width];
            images = new PictureBox[height, width];
            smileX = 0;
            smileY = 2;
            Generate();
        }

        private void Generate() //алгоритм генерации картинок
        {
            //int totalMedals = 0; //общее количество медалей в лабиринте
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //текущий объект лабиринта по дефолту - коридор
                    MazeObject.MazeObjectType current = MazeObject.MazeObjectType.HALL;

                    // в 1 случае из 5 - ставим стену (вероятность в 20% геренируется препятствие)
                    if (r.Next(5) == 0)
                    {
                        current = MazeObject.MazeObjectType.WALL;
                    }

                    // в 1 случае из 250 - кладём денежку (вероятность 1 на 250)
                    if (r.Next(250) == 0)
                    {
                        current = MazeObject.MazeObjectType.MEDAL;
                        medalCount++;
                    }

                    // в 1 случае из 250 - размещаем врага
                    if (r.Next(100) == 0)
                    {
                        current = MazeObject.MazeObjectType.ENEMY;
                    }

                    // в 1 случае из 250 - размещаем лекарство
                    if (r.Next(250) == 0)
                    {
                        current = MazeObject.MazeObjectType.HEART;
                    }

                    // в 1 случае из 250 - размещаем кофе
                    if (r.Next(250) == 0)
                    {
                        current = MazeObject.MazeObjectType.COFFEE;
                    }

                    // стены по периметру обязательны
                    if (y == 0 || x == 0 || y == height - 1 | x == width - 1)
                    {
                        current = MazeObject.MazeObjectType.WALL;
                    }

                    // наш персонажик
                    if (x == smileX && y == smileY)
                    {
                        current = MazeObject.MazeObjectType.CHAR;
                    }

                    // есть выход, и соседняя ячейка справа всегда свободна
                    if (x == smileX + 1 && y == smileY || x == width - 1 && y == height - 3)
                    {
                        current = MazeObject.MazeObjectType.HALL;
                    }

                    //без этого кода на лабиринтах побольше иногда рядом с ячейкой выхода генерируется стена
                    if (x == width - 2 && y == height - 3 && current!=MazeObject.MazeObjectType.HALL)
                    {
                        current = MazeObject.MazeObjectType.HALL;
                    }

                    maze[y, x] = new MazeObject(current);
                    images[y, x] = new PictureBox();
                    images[y, x].Location = new Point(x * maze[y, x].width, y * maze[y, x].height);
                    images[y, x].Parent = parent;
                    images[y, x].Width = maze[y, x].width;
                    images[y, x].Height = maze[y, x].height;
                    images[y, x].BackgroundImage = maze[y, x].texture;
                    images[y, x].Visible = false;//обеспечивает, что по результату работы методе Generate() не будет показаны ЭУ
                }
            }
        }

        public void Show()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    images[y, x].Visible = true;
                }
            }
        }



    }
}
