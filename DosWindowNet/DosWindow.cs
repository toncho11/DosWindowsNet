using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DosWindowNet
{
    public class DosWindow
    {
        protected int posx;
        protected int posy;

        protected int width;
        protected int height;

        protected string title;

        protected bool isVisible;
        protected bool showBorder;
        public bool SkipTabOrder;
        protected bool showCursor;

        protected StringBuilder text;

        protected ConsoleColor bgColor;
        protected ConsoleColor fgColor;

        public DosWindow(int posx, int posy, int width, int height, string title)
        {
            this.posx = posx;
            this.posy = posy;
            this.width = width;
            this.height = height;
            this.title = title;

            isVisible = false;
            showBorder = true;

            bgColor = ConsoleColor.DarkBlue;
            fgColor = ConsoleColor.White;

            SkipTabOrder = false;
            showCursor = true;

            Window.List.Add(this);
        }

        public virtual void Draw()
        {
            this.ApplyStyle();

            //top
            if (showBorder)
            {
                Console.SetCursorPosition(posx, posy);

                if (!string.IsNullOrWhiteSpace(title))
                {
                    int leftTopLine = (width - title.Length - 4) / 2;

                    Console.Write("╔" + DrawLine(leftTopLine, "═") + "[ " + title + " ]" + DrawLine(width - leftTopLine - title.Length - 4, "═") + "╗");
                }
                else Console.Write("╔" + DrawLine(width, "═") + "╗");
            }

            //body
            Console.SetCursorPosition(posx, posy + 1);
            for (int i = 0; i < height; i++)
            {
                if (showBorder) Console.Write("║");

                for (int j = 0; j < width; j++) Console.Write(" ");

                if (showBorder) Console.Write("║");

                Console.SetCursorPosition(posx, posy + i + 1);
            }

            //bottom
            if (showBorder)
            {
                Console.SetCursorPosition(posx, posy + height);

                Console.Write("╚" + DrawLine(width, "═") + "╝");
            }

            //set position for text output
            Console.SetCursorPosition(posx + 1, posy + 1);

            isVisible = true;
        }

        protected static string DrawLine(int i,string character)
        {
            string p = "";

            for (int c = 0; c < i; c++)
                p = p + character;

            return p;
        }

        public void Hide()
        {
            Console.BackgroundColor = Dekstop.CurrentBackgroundColor;
            Console.ForegroundColor = Dekstop.CurrentForegroundColor;

            for (int j = 0; j < height + 1; j++)
            {
                Console.SetCursorPosition(posx, posy + j);

                string line = string.Empty;
                for (int i = 0; i < width + 2; i++)
                {
                    line += Dekstop.matrix[i, j];

                }
                Console.Write(line);
            }

            isVisible = false;
        }


        public virtual bool ProcessKeyboardEvent(ConsoleKeyInfo key)
        {
            bool Processed = false;
            return Processed;
        }

        public bool IsVisible
        {
            get
            {
                return isVisible;
            }
        }

        public virtual void SetToFocus()
        {
            //TODO: change color of window
            if (showBorder)
                Console.SetCursorPosition(posx + 1, posy + 1);
            else Console.SetCursorPosition(posx, posy+1);

            Console.CursorVisible = showCursor;
        }

        public virtual string Text
        {
            get
            {
                return text.ToString();
            }
            set
            {
                this.ApplyStyle();

                Console.SetCursorPosition(posx + 1, posy + 1);
                Console.Write(value);
                text = new StringBuilder(value);
            }
        }

        protected void ApplyStyle()
        {
            Console.BackgroundColor = bgColor;
            Console.ForegroundColor = fgColor;
            Console.CursorVisible = showCursor;
        }
    }
}
