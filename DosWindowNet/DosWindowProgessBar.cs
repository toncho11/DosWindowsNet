using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DosWindowNet
{
    public class DosWindowProgessBar : DosWindow
    {
        public DosWindowProgessBar(int size, string title)
            : base((Console.WindowWidth - size) / 2, Console.WindowHeight - 8, size + 2, 2, title)
        {
            base.title = title;
            SkipTabOrder = true;
            showCursor = false;
        }

        //protected override void RegisterWindow()
        //{

        //}

        public override void Draw()
        {
            Console.CursorVisible = base.showCursor;

            base.Draw();
        }

        public void SetProgress(int progress)
        {
            Console.SetCursorPosition(posx + 1, posy + 1);

            Console.Write(GetLine(progress, "▓"));
        }
    }
}
