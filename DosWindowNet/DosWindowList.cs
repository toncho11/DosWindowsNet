using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DosWindowNet
{
    public class DosWindowList : DosWindow
    {
        public DosWindowList(int posx, int posy, int width, int height, string title)
            : base(posx,posy,width,height,title)
        {
            //Register
            //Window.List.Add(this);
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
        }

        public override bool ProcessKeyboardEvent(ConsoleKeyInfo key)
        {
            return false;
        }

        public override void Draw()
        {
            base.Draw();

            Console.CursorVisible = base.showCursor;
        }
    }
}
