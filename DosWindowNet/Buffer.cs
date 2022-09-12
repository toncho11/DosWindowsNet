using System;

namespace DosWindowNet
{
    public class Buffer
    {
        //TODO set the correct size
        static char[,] CHAR = new char[200,200];
        static ConsoleColor[,] FGC = new ConsoleColor[200,200];
        static ConsoleColor[,] BGC = new ConsoleColor[200, 200];

        static int X;
        static int Y;

        public static bool DoNotOverWrite;

        public static void SetCursorPosition(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            X = x;
            Y = y;
        }

        public static void Write(char ch)
        {
            Console.Write(ch);

            if (!DoNotOverWrite)
            {
                CHAR[X, Y] = ch;
                FGC[X, Y] = Console.ForegroundColor;
                BGC[X, Y] = Console.BackgroundColor;
            }
            X = X + 1;
        }

        public static void Write(string s)
        {
            for (int i=0;i < s.Length; i++)
            {
                Write(s[i]);
            }
        }

        public static char Get(int x, int y, out ConsoleColor bgc, out ConsoleColor fgc)
        {
            bgc = BGC[x, y];
            fgc = FGC[x, y];
            return CHAR[x, y];
        }

        public static void SetAll(ConsoleColor bgc)
        {
            for (int i = 0;i< 200;i++)
            {
                for (int j = 0; j < 200; j++)
                {
                    BGC[i, j] = bgc;
                }
            }
        }
    }
}