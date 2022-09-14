using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DosWindowNet
{
    public class DosWindowMessage : DosWindow
    {
        public DosWindowMessage(int posx, int posy, int width, int height, string title)
            : base(posx,posy,width,height,title)
        {
            SkipTabOrder = true;
        }

        public DosWindowMessage(string text, string title)
            : base((Console.WindowWidth - text.Length) / 2, 20, text.Length + 2, 2, title)
        {
            base.title = title;
            base.text = new StringBuilder(text);
            SkipTabOrder = true;
            showCursor = false;
            base.bgColor = ConsoleColor.DarkRed;
        }

        public DosWindowMessage(int posy, string text, string title)
           : base((Console.WindowWidth - text.Length) / 2, posy, text.Length + 2, 2, title)
        {
            base.title = title;
            base.text = new StringBuilder(text);
            SkipTabOrder = true;
            showCursor = false;
            base.bgColor = ConsoleColor.DarkRed;
        }

        public override void Draw()
        {
            this.Save();

            base.Draw();

            Console.SetCursorPosition(posx + 1, posy + 1);

            Console.Write(" " + text + " ");

            //wait for key pressed
            Console.ReadKey();

            //base.Hide();
            base.Restore();
        }
    }
}
