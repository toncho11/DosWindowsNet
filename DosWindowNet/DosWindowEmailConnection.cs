using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DosWindowNet
{
    public class DosWindowEmailConnection : DosWindow
    {
        public DosWindowEmailConnection(int posx, int posy, int width, int height, string title)
            : base(posx,posy,width,height,title)
        {
            SkipTabOrder = true;
            showCursor = false;
        }

        public override void Draw()
        {
            base.Draw();

            Console.SetCursorPosition(posx + 1, posy + 1);
            Console.Write("Email address:");

            Console.SetCursorPosition(posx + 1, posy + 3);
            Console.Write("Password:");

            Console.SetCursorPosition(posx + 40, posy + 1);
            Console.Write("Server:");

            Console.SetCursorPosition(posx + 40, posy + 3);
            Console.Write("Port:");
        }
    }
}
