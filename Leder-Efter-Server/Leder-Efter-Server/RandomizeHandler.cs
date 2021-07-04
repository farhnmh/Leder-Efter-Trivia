using System;
using System.Collections.Generic;
using System.Text;

namespace Leder_Efter_Server
{
    class RandomizeDatabase
    {
        public static List<string> stuff = new List<string> { "Grape", "Banana", "Apple" };
        public static List<string> color = new List<string> { "Merah", "Hijau", "Biru" };
        public static List<string> textColor = new List<string> { "Merah", "Hijau", "Biru" };
    }

    class RandomizeHandler
    {
        public static int indexColor, indexText;

        public static string StuffRandomizer()
        {
            Random random = new Random();
            int index = random.Next(RandomizeDatabase.stuff.Count);
            var result = RandomizeDatabase.stuff[index];
            RandomizeDatabase.stuff.RemoveAt(index);
            return result;
        }

        public static string ColorRandomizer()
        {
            Random random = new Random();
            //int index = random.Next(RandomizeDatabase.color.Count);
            indexColor = random.Next(RandomizeDatabase.color.Count);
            //var result = RandomizeDatabase.color[index];
            //RandomizeDatabase.color.RemoveAt(index);
            var result = RandomizeDatabase.color[indexColor];
            Console.WriteLine("Color: " + result);    
            return result;
        }

        public static string TextColor()
        {
            Random random = new Random();
            //int index = random.Next(RandomizeDatabase.textColor.Count);
            indexText = random.Next(RandomizeDatabase.textColor.Count);
            //var result = RandomizeDatabase.textColor[index];
            //RandomizeDatabase.textColor.RemoveAt(index);
            var result = RandomizeDatabase.textColor[indexText];
            Console.WriteLine("Text: " + result);
            return result;
        }

        //public static void DeleteList()
        //{
        //    RandomizeDatabase.color.RemoveAt(indexColor);
        //    RandomizeDatabase.textColor.RemoveAt(indexText);

        //    if (RandomizeDatabase.color.Count <= 0 || RandomizeDatabase.textColor.Count <= 0)
        //    {

        //    }
        //}
    }
}
