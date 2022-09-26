using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;
using System.Collections;
using System.IO;

using DosWindowNet;

namespace fm
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(80, 25);

            if (Console.WindowWidth < 80 || Console.WindowHeight < 25)
            {
                Console.WriteLine("At least a terminal window of 80 x 25 is required." + Environment.NewLine + "Press any key to continue anyway.");
                Console.ReadKey();
            }

            DosWindow win = new DosWindow(0, 34, 78, 4, "");
            win.Draw();
            win.Text = "   Copy Move cOmp Find Rename Delete Ver view/Edit Attrib Wordp Print List    ";

            DosWindow win2 = new DosWindow(0, 31, 78, 3, "");
            win2.Draw();
            win2.Text = "";
            Console.ReadLine();
        }
    }
}
