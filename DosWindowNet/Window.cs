using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DosWindowNet
{
    public static class Window
    {
        public static List<DosWindow> List;
        static int WindowIndex;

        static Window()
        {
            List = new List<DosWindow>();
            WindowIndex = 0;
        }

        //public static void SetCurrentWindow(DosWindow win)
        //{

        //}

        public static DosWindow GetCurrentWindow()
        {
            DosWindow win = List[WindowIndex % List.Count];
            return win;
        }

        public static void GiveFocusToNextWindow()
        {
            WindowIndex = WindowIndex + 1;

            //infinite loop if all are invisible
            while (List[WindowIndex % List.Count].IsVisible == false || List[WindowIndex % List.Count].SkipTabOrder == true)
                WindowIndex = WindowIndex + 1;

            List[WindowIndex % List.Count].SetToFocus();
        }
    }
}
