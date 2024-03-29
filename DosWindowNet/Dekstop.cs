﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DosWindowNet
{
    public static class Dekstop
    {
        public static string[,] matrix;

        static Dekstop()
        {
            //matrix = new string[Console.WindowWidth, Console.WindowHeight];
        }

        public static void Draw()
        {
            //░ ▒ ▓

            ConsoleColor oldBGColor = Console.BackgroundColor;
            ConsoleColor oldFgColor = Console.ForegroundColor;

            Console.BackgroundColor = CurrentBackgroundColor;
            Console.ForegroundColor = CurrentForegroundColor;

            Console.Clear();

            var w = Console.WindowWidth;
            var h = Console.WindowHeight;

            ScrBuffer.DoNotOverWrite = true;

            ScrBuffer.SetAll(CurrentBackgroundColor);

            for (int j = 0; j < (h - 0); j++)
            {
                for (int i = 0; i < w; i++)
                {
                    ScrBuffer.Write("▓");
                }
            }

            ScrBuffer.DoNotOverWrite = false;

            Console.BackgroundColor = oldBGColor;
            Console.ForegroundColor = oldFgColor;
        }

        public static ConsoleColor CurrentBackgroundColor
        {
            get
            {
                return ConsoleColor.Black;
            }
        }
        public static ConsoleColor CurrentForegroundColor
        {
            get
            {
                return ConsoleColor.Black;
            }
        }
    }
}
