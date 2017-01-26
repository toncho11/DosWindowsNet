using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DosWindowNet
{
    public class DosWindowMessage : DosWindow
    {
        protected string text;

        public DosWindowMessage(int posx, int posy, int width, int height, string title)
            : base(posx,posy,width,height,title)
        {
            SkipTabOrder = true;
        }

        public DosWindowMessage(string text, string title)
            : base((Console.WindowWidth - text.Length) / 2, 20, text.Length + 2, 2, title)
        {
            base.title = title;
            this.text = text;
            SkipTabOrder = true;
            showCursor = false;
        }

        protected override void RegisterWindow()
        {
           
        }

        public override void Draw()
        {
            base.Draw();

            Console.CursorVisible = base.showCursor;

            Console.BackgroundColor = ConsoleColor.DarkBlue;

            Console.SetCursorPosition(posx + 1, posy + 1);

            Console.Write(" " + text + " ");

            //wait for key pressed
            Console.ReadKey();

            base.Hide();
        }
    }
}
