using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DosWindowNet
{
    public delegate void ChangedEventHandler(object sender, int index);

    public class DosWindowList : DosWindow
    {
        int currentDow;
        int maxRowDown;

        List<string> list;
        public event ChangedEventHandler RowIndexChanged;

        public DosWindowList(int posx, int posy, int width, int height, string title)
            : base(posx,posy,width,height,title)
        {
            //Register
            //Window.List.Add(this);
            showCursor = false;
        }

        public void LoadList(List<string> list)
        {
            System.Text.Encoding oldEncoding = Console.OutputEncoding;
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            for (int i = 0; i < list.Count; i++)
            {
                Console.SetCursorPosition(posx + 1, posy + i + 1);
                Console.Write(list[i]);
            }

            Console.OutputEncoding = oldEncoding;

            maxRowDown = list.Count;
            currentDow = 0;

            this.list = list;

            if (list.Count>0)
                SelectRow(true, 0);
        }

        public override bool ProcessKeyboardEvent(ConsoleKeyInfo ki)
        {
            Console.BackgroundColor = bgColor;
            Console.ForegroundColor = fgColor;

            bool processed = false;

            //move down
            if (ki.Key == ConsoleKey.DownArrow)
            {
                if ((currentDow + 1) < Math.Min(maxRowDown, base.width))
                {
                    SelectRow(false, currentDow);

                    currentDow = currentDow + 1;

                    SelectRow(true, currentDow);
                }
                processed = true;
            }
            else
            //move up
            if (ki.Key == ConsoleKey.UpArrow && (currentDow - 1) >= 0)
            {
                SelectRow(false, currentDow);

                currentDow = currentDow - 1;

                SelectRow(true, currentDow);

                processed = true;
            }

            return processed;
        }

        public void SelectRow(bool isSelected, int index)
        {
            //select //deselect row

            if (isSelected)
            {
                RowIndexChanged?.Invoke(this, index);

                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.DarkBlue;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.SetCursorPosition(posx + 1, posy + 1 + index);
            Console.Write(list[index]);

        }

        public override void Draw()
        {
            base.Draw();

            Console.BackgroundColor = bgColor;
            Console.ForegroundColor = fgColor;
            Console.CursorVisible = base.showCursor;
        }
    }
}
