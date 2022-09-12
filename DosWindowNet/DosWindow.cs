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

        protected string[] previousScreen = null;

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

        public void Save()
        {
            Buffer.DoNotOverWrite = true;
        }

        public virtual void Draw()
        {
            this.ApplyStyle();

            //top
            if (showBorder)
            {
                //Console.SetCursorPosition(posx, posy);
                Buffer.SetCursorPosition(posx, posy);

                if (!string.IsNullOrWhiteSpace(title))
                {
                    int leftTopLine = (width - title.Length - 4) / 2;

                    Buffer.Write("╔" + DrawLine(leftTopLine, "═") + "[ " + title + " ]" + DrawLine(width - leftTopLine - title.Length - 4, "═") + "╗");
                }
                else Buffer.Write("╔" + DrawLine(width, "═") + "╗");
            }

            //body
            Buffer.SetCursorPosition(posx, posy + 1);
            for (int i = 0; i < height; i++)
            {
                if (showBorder) Buffer.Write("║");

                for (int j = 0; j < width; j++) Buffer.Write(" ");

                if (showBorder) Buffer.Write("║");

                Buffer.SetCursorPosition(posx, posy + i + 1);
            }

            //bottom
            if (showBorder)
            {
                Buffer.SetCursorPosition(posx, posy + height);

                Buffer.Write("╚" + DrawLine(width, "═") + "╝");
            }

            //set position for text output
            Buffer.SetCursorPosition(posx + 1, posy + 1);

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

        /// <summary>
        /// The better version of hide
        /// </summary>
        public void Restore() 
        {

            for (int y = 0; y< (height+1);y++)
            {
                Console.SetCursorPosition(posx, posy + y);

                for (int x=0; x < (width + 2); x++)
                {
                    ConsoleColor fgc;
                    ConsoleColor bgc;
                    char ch = Buffer.Get(posx + x, posy + y, out bgc, out fgc);
                    Console.ForegroundColor = fgc;
                    Console.BackgroundColor = bgc;
                    Console.Write(ch);
                }
            }

            Buffer.DoNotOverWrite = false;

        }
    }
}
