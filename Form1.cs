using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Schema;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Maze
{
    public partial class Form1 : Form
    {
        //размеры лабиринта в ячейках (16х16 пикселей)
        int columns = 40; 
        int rows = 20;

        //размеры картинок
        int pictureSize = 16;//ширина и высота ячейки
        Labirint l; //ccылка на логику происходящего в лабиринте
        public Form1()
        {
            InitializeComponent();//базовые настройки формы
            Options();//кастомизированный метод, который меняет заголовок для окна
            StartGame();
        }

        public void Options()
        {
            //Text = "Колобок(**) Медали: " ;

            BackColor = Color.FromArgb(255, 92, 118, 137);//цвет бЭкграунда
             
            //размеры окна
            //Width = columns * 16 + 16;//16 - размер картинки в пикселях
            //Height = rows * 16 + 40;

            //Размеры клиетской области (т.е. того участка, на котором размещаются элементы управления)
            ClientSize = new Size(columns*pictureSize, rows*pictureSize);

            //свойство, которое позволяет размещать окно четко по центру рабочего стола
            StartPosition = FormStartPosition.CenterScreen;
        }

       
        public void StartGame() {
            l = new Labirint(this, columns, rows); //создается объект с типом данных Лабиринт,
            //которому сообщается кто форма (родитель для всех дочерних элементов)
            //какого размера лабиринт в ячейках по ширине и высоте
            l.Show();
            SetTitle();
        }

        private void SetTitle()
        {
            Text = $"Колобок(**) Медали: {l.medalCount} Здоровье: {l.smileHealth} Энергия: {l.smileEnergy}" ;
        }

        private void ChangeTitle()
        {
            Text = $"Колобок(**) Медали: {l.medalCount} Здоровье: {l.smileHealth} Энергия: {l.smileEnergy}";
        }

        private void CheckAllMedalCollect(int medalCount)
        {
            if (medalCount == 0) 
            {
                MessageBox.Show("Все медали собраны!", "Победа!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
        }

        private void CheckHealth()
        {
            if (l.smileHealth<=0)
            {
                MessageBox.Show("Закончилось здоровье", "Поражение!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
        }
        private void CheckEnd()
        {
            if (l.smileX==l.width - 1 &&  l.smileY==l.height - 3)
            {
                MessageBox.Show("Достигнут выход из лабиринта!", "Победа!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
        }
        private void CheckEnergy()
        {
            if (l.smileEnergy<=0) 
            {
                MessageBox.Show("Энергия закончилась!", "Поражение!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
        }
        public void SmileXPlus()
        {
            l.images[l.smileY, l.smileX].BackgroundImage = MazeObject.images[0];
            l.smileX++;
            l.images[l.smileY, l.smileX].BackgroundImage = MazeObject.images[4];
        }

        public void SmileXMinus()
        {
            l.images[l.smileY, l.smileX].BackgroundImage = MazeObject.images[0];
            l.smileX--;
            l.images[l.smileY, l.smileX].BackgroundImage = MazeObject.images[4];
        }

        public void SmileYMinus()
        {
            l.images[l.smileY, l.smileX].BackgroundImage = MazeObject.images[0];
            l.smileY--;
            l.images[l.smileY, l.smileX].BackgroundImage = MazeObject.images[4];
        }

        public void SmileYPlus()
        {
            l.images[l.smileY, l.smileX].BackgroundImage = MazeObject.images[0];
            l.smileY++;
            l.images[l.smileY, l.smileX].BackgroundImage = MazeObject.images[4];
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {  
            Random health=new Random();
            
            if (e.KeyCode == Keys.Right) 
            {
                l.smileStep++;
                l.smileEnergy--;
                ChangeTitle();
                CheckEnergy();
                //проверка, свододна ли ячейка справа
                if (l.maze[l.smileY, l.smileX+1].type == 
                    MazeObject.MazeObjectType.HALL) //проверяем ячейка правее на одну позицию,
                                                    //является ли она коридором
                {
                    SmileXPlus();
                    CheckEnd();
                }
                else if (l.maze[l.smileY, l.smileX+1].type==MazeObject.MazeObjectType.MEDAL)
                {
                    SmileXPlus();
                    l.medalCount--;
                    ChangeTitle();
                    CheckAllMedalCollect(l.medalCount);
                }
                else if (l.maze[l.smileY, l.smileX + 1].type == MazeObject.MazeObjectType.ENEMY)
                {
                    SmileXPlus();
                    l.smileHealth-=health.Next(20,25);
                    ChangeTitle();
                    CheckHealth();
                }
                else if (l.maze[l.smileY, l.smileX + 1].type == MazeObject.MazeObjectType.HEART)
                {
                    if (l.smileHealth<100)
                    {
                        SmileXPlus();
                        l.smileHealth += 5;
                        ChangeTitle();
                        CheckHealth();
                    }
                }
                else if (l.maze[l.smileY, l.smileX + 1].type == MazeObject.MazeObjectType.COFFEE)
                {
                    if (l.smileStep >10)
                    {
                        SmileXPlus();
                        l.smileEnergy += 25;
                        ChangeTitle();
                    }
                }

            }
            else if (e.KeyCode == Keys.Left)
            {
                l.smileStep++;
                l.smileEnergy--;
                ChangeTitle();
                if (l.maze[l.smileY, l.smileX - 1].type ==
                    MazeObject.MazeObjectType.HALL) //проверяем является ли ячейка левее коридором
                {
                    SmileXMinus();
                }
                else if (l.maze[l.smileY, l.smileX - 1].type == MazeObject.MazeObjectType.MEDAL)
                {
                    SmileXMinus();
                    l.medalCount--;
                    ChangeTitle();
                    CheckAllMedalCollect(l.medalCount);
                }
                else if (l.maze[l.smileY, l.smileX -1].type == MazeObject.MazeObjectType.ENEMY)
                {
                    SmileXMinus();
                    l.smileHealth -= health.Next(20, 25);
                    ChangeTitle();
                    CheckHealth();
                }
                else if (l.maze[l.smileY, l.smileX - 1].type == MazeObject.MazeObjectType.HEART)
                {
                    if(l.smileHealth<100)
                    {
                        SmileXMinus();
                        l.smileHealth += 5;
                        ChangeTitle();
                        CheckHealth();
                    }
                }
                else if (l.maze[l.smileY, l.smileX - 1].type == MazeObject.MazeObjectType.COFFEE)
                {
                    if (l.smileStep > 10)
                    {
                        SmileXMinus();
                        l.smileEnergy += 25;
                        ChangeTitle();
                    }
                }

            }
            else if(e.KeyCode == Keys.Up) 
            {
                l.smileStep++;
                l.smileEnergy--;
                ChangeTitle();
                if (l.maze[l.smileY-1, l.smileX ].type ==
                    MazeObject.MazeObjectType.HALL) //проверяем является ли ячейка выше на одну позицию, коридором
                {
                    SmileYMinus();
                    CheckEnd();
                }
                else if (l.maze[l.smileY-1, l.smileX].type == MazeObject.MazeObjectType.MEDAL)
                {
                    SmileYMinus();
                    l.medalCount--;
                    ChangeTitle();
                    CheckAllMedalCollect(l.medalCount);
                }
                else if (l.maze[l.smileY - 1, l.smileX].type == MazeObject.MazeObjectType.ENEMY)
                {
                    SmileYMinus();
                    l.smileHealth -= health.Next(20, 25);
                    ChangeTitle();
                    CheckHealth();
                }
                else if (l.maze[l.smileY - 1, l.smileX].type == MazeObject.MazeObjectType.HEART)
                {
                    if (l.smileHealth<100)
                    {
                        SmileYMinus();
                        l.smileHealth += 5;
                        ChangeTitle();
                        CheckHealth();
                    }
                }
                else if (l.maze[l.smileY - 1, l.smileX].type == MazeObject.MazeObjectType.COFFEE)
                {
                    if (l.smileStep > 10)
                    {
                        SmileYMinus();
                        l.smileEnergy += 25;
                        ChangeTitle();
                    }
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                l.smileStep++;
                l.smileEnergy--;
                ChangeTitle();
                if (l.maze[l.smileY +1, l.smileX].type ==
                    MazeObject.MazeObjectType.HALL) //проверяем является ли ячейка выше на одну позицию, коридором
                {
                    SmileYPlus();
                    CheckEnd();
                }
                else if (l.maze[l.smileY+1, l.smileX].type == MazeObject.MazeObjectType.MEDAL)
                {
                    SmileYPlus();
                    l.medalCount--;
                    ChangeTitle();
                    CheckAllMedalCollect(l.medalCount);
                }
                else if (l.maze[l.smileY+1, l.smileX].type == MazeObject.MazeObjectType.ENEMY)
                {
                    SmileYPlus();
                    l.smileHealth -= health.Next(20, 25);
                    ChangeTitle();
                    CheckHealth();
                }
                else if (l.maze[l.smileY + 1, l.smileX].type == MazeObject.MazeObjectType.HEART)
                {
                    if(l.smileHealth < 100) 
                    {
                        SmileYPlus();
                        l.smileHealth += 5;
                        ChangeTitle();
                        CheckHealth();
                    }
                }
                else if (l.maze[l.smileY + 1, l.smileX].type == MazeObject.MazeObjectType.COFFEE)
                {
                    if (l.smileStep > 10)
                    {
                        SmileYPlus();
                        l.smileEnergy += 25;
                        ChangeTitle();
                    }
                }
            }
        }
    }
}
