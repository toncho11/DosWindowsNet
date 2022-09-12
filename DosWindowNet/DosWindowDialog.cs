using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DosWindowNet
{
    public class DosWindowDialog : DosWindow
    {
        public delegate void Notify(ConsoleKeyInfo key);

        public event Notify KeyPressed;

        public DosWindowDialog(int posx, int posy, int width, int height, string title)
            : base(posx, posy, width, height, title)
        {
            SkipTabOrder = true;
        }

        public DosWindowDialog(string text, string title)
            : base((Console.WindowWidth - text.Length) / 2, 20, text.Length + 2, 2, title)
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
            var key = Console.ReadKey();

            KeyPressed(key);

            //base.Hide();
            base.Restore();
        }
    }
}
