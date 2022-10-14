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
        static DosWindowMessage winAbout;
        static DosWindowExit winExit;

        static ConsoleColor oldBgColor;
        static ConsoleColor oldFgColor;

        static bool exitRequested = false;

        class DosWindowExit : DosWindowDialog
        {
            public DosWindowExit(int posx, int posy, int width, int height, string text, string title) : base(posx, posy, width, height, text, title)
            {
            }

            public override void DrawText()
            {
                ScrBuffer.SetCursorPosition(29, 23);
                ScrBuffer.Write("Are you SURE you want to");

                ScrBuffer.SetCursorPosition(29, 24);
                ScrBuffer.Write("exit File Manager? (Y/N)");
            }
        }

        static void Main(string[] args)
        {
            Console.SetWindowSize(80, 25);

            if (Console.WindowWidth < 80 || Console.WindowHeight < 25)
            {
                Console.WriteLine("At least a terminal window of 80 x 25 is required." + Environment.NewLine + "Press any key to continue anyway.");
                Console.ReadKey();
            }

            BuildUI();

            Loop();

            Exit();
        }

        public static void BuildUI()
        {
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
        }

        public static void Loop()
        {
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                    //First process keyboard events for the local window
                    bool isProcessedByWindow = Window.GetCurrentWindow().ProcessKeyboardEvent(keyInfo);

                    if (isProcessedByWindow) continue;

                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        winExit = new DosWindowExit(20, 22, 40,3,"", "");
                        winExit.KeyPressed += WinExit_KeyPressed;
                        winExit.Draw();

                        if (exitRequested)
                            break;
                    }
                    else
                    if (keyInfo.Key == ConsoleKey.Tab || keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        Window.GiveFocusToNextWindow();
                    }
                    else
                    if (keyInfo.Key == ConsoleKey.F3)
                    {
                        winAbout = new DosWindowMessage("File Manager by Anton Andreev v1.0", "About");
                        winAbout.SetColors(ConsoleColor.Green, ConsoleColor.White);
                        winAbout.Draw();
                    }

                    //Refocus
                    Window.GetCurrentWindow().SetToFocus();
                }
                else
                {
                    System.Threading.Thread.Sleep(30);
                }
            }
        }

        public static void Exit()
        {
            Console.BackgroundColor = oldBgColor;
            Console.ForegroundColor = oldFgColor;
            Console.Clear();
            Console.WriteLine("Bye.");
        }

        private static void WinExit_KeyPressed(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.Y)
            {
                exitRequested = true;
            }
        }
    }
}
