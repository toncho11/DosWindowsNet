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

            //top blue line
            DosWindow win4 = new DosWindow(0, 0, 80, 1, "");
            win4.SetColors(ConsoleColor.DarkBlue, ConsoleColor.White);
            win4.showBorder = false;
            win4.Draw();
            win4.Text = "File Manager 1.0 " + DateTime.Today.ToShortDateString();

            DosWindow win5 = new DosWindow(0, 1, 80, 1, "");
            win5.SetColors(ConsoleColor.DarkBlue, ConsoleColor.White);
            win5.showBorder = false;
            win5.Draw();
            string tt = "                              File Functions                                   ".Replace(" ", "\u2500");
            win5.Text = tt;

            //path
            DosWindow win6 = new DosWindow(0, 2, 80, 1, "");
            win6.SetColors(ConsoleColor.Black, ConsoleColor.White);
            win6.showBorder = false;
            win6.Draw();
            win6.Text = @"Path=C:\*.*";

            //top green line
            DosWindow win3 = new DosWindow(0, 3, 80, 1, "");
            win3.SetColors(ConsoleColor.Green, ConsoleColor.White);
            win3.showBorder = false;
            win3.Draw();
            win3.Text = "    Name     Ext     Size";

            DosWindow win = new DosWindow(0, 21, 78, 4, "");
            win.Draw();
            win.Text = "   Copy Move cOmp Find Rename Delete Ver view/Edit Attrib Wordp Print List    ";

            DosWindow win2 = new DosWindow(0, 18, 78, 3, "");
            win2.Draw();
            win2.Text = "";

            Console.ReadLine();
        }
    }
}
