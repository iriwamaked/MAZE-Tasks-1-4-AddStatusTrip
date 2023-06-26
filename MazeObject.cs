using System;
using System.Drawing;

namespace Maze
{
    class MazeObject
    {
        //публичное перечисление с типами объектов лабиринта
        public enum MazeObjectType { HALL, WALL, MEDAL, ENEMY, CHAR, HEART, COFFEE };

        //массив картинок (приватное, только для чтения)
        public static Bitmap[] images = {new Bitmap(Properties.Resources.hall),
            new Bitmap(Properties.Resources.wall),
            new Bitmap(Properties.Resources.medal),
            new Bitmap(Properties.Resources.enemy),
            new Bitmap(Properties.Resources.player),
            new Bitmap(Properties.Resources.heart),
            new Bitmap(Properties.Resources.coffee)};
        //поле с типом перечисления, каждому объекту будет задан его тип
        public MazeObjectType type { get; set; }
        //каждой картинке присваивается ширина, высота, какая-та текстура
        public int width { get; set; }
        public int height { get;set; }
        public Image texture {get; set; }//ссылка на саму картинку

        public MazeObject(MazeObjectType type)//конструктор, в который передается тип объекта
        {
            this.type = type;//запоминаю тип объекта
            width = 16;
            height = 16;
            texture = images[(int)type];//в качестве картинки берется тип из перечисления, перевожу его в int
                                        //запоминаю картинку из массива картинок
        }

    }
}
