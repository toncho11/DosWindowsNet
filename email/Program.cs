using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;
using System.Collections;
using System.IO;

using S22.Imap;
using DosWindowNet;

namespace email
{
    //https://github.com/Automattic/cli-table
    //http://stackoverflow.com/questions/17619279/extended-ascii-in-c-sharp
    //https://en.wikipedia.org/wiki/Code_page_437 - contains the characters used to draw the windows
    //For Bulgarian Font must be changed to Consolas on Windows
    //http://stackoverflow.com/questions/7524057/how-do-i-change-the-full-background-color-of-the-console-window-in-c
    //https://github.com/auriou/FIGlet - output text using ascii art

    class Program
    {
        #region Windows declarations
        static DosWindowEmailConnection winCredentials;
        static DosWindowList winEmailList;
        static DosWindow winEmailBody;
        static DosWindowMessage winAbout;
        static DosWindowDialog winExit;
        static DosWindowTextBox tbEmailAddress;
        static DosWindowTextBox tbPassword;
        static DosWindowTextBox tbServer;
        static DosWindowTextBox tbPort;
        #endregion

        static Hashtable htConnection;
        static System.Net.Mail.MailMessage[] messages;

        static ConsoleColor oldBgColor;
        static ConsoleColor oldFgColor;

        static bool shouldExit = false;

        public static void Initialize()
        {
            Dekstop.Draw();

            winCredentials = new DosWindowEmailConnection(3, 3, 70, 4, "Login");
            winCredentials.Draw();

            tbEmailAddress = new DosWindowTextBox(19, 3, 22, 1, ConsoleColor.Blue);
            tbEmailAddress.Draw();

            tbPassword = new DosWindowTextBox(19, 5, 22, 1, ConsoleColor.Blue);
            tbPassword.Draw();

            tbServer = new DosWindowTextBox(51, 3, 22, 1, ConsoleColor.Blue);
            tbServer.Draw();

            tbPort = new DosWindowTextBox(51, 5, 5, 1, ConsoleColor.Blue);
            tbPort.Draw();

            winEmailList = new DosWindowList(3, 9, 70, 10, "Message list");
            winEmailList.RowIndexChanged += WinEmailList_RowIndexChanged;
            winEmailList.Draw();

            winEmailBody = new DosWindow(3, 21, 70, 10, "Message body");
            winEmailBody.Draw();

            CreateTopMenu();
            Window.GiveFocusToNextWindow();
        }

        private static void WinEmailList_RowIndexChanged(object sender, int index)
        {
            winEmailBody.Draw(); //clear
            string body = messages[index].Body;
            winEmailBody.Text = body.Substring(0,80); //TODO: remove the restrictions of 80 characters after adding word wraping and scrolling !
        }

        public static void StartLoop()
        {
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                    //First process keyboard events for the local window
                    bool isProcessedByWindow = Window.GetCurrentWindow().ProcessKeyboardEvent(keyInfo);

                    if (isProcessedByWindow) continue;

                    //Next process keybaord events for the application
                    if (keyInfo.Key == ConsoleKey.F2)
                    {
                        ReloadMails();
                    }
                    else
                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        winExit = new DosWindowDialog("Do you want to exit Y/N?", "Exit");
                        winExit.KeyPressed += WinExit_KeyPressed;
                        winExit.Draw();

                        if (shouldExit)
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
                        winAbout = new DosWindowMessage("Console e-mail client by Anton Andreev v1.0", "About");
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


        private static void WinExit_KeyPressed(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.Y)
            {
                shouldExit = true;
            }
        }

        static void Main(string[] args)
        {
            //Console.SetBufferSize(80, 40); //not clear why it does not accept an equal to the screen value
			Console.SetWindowSize(80, 40);
			
            if (Console.WindowWidth<80 || Console.WindowHeight < 25)
            {
                Console.WriteLine("At least a terminal window of 80 x 25 is required." + Environment.NewLine+ "Press any key to continue anyway.");
                Console.ReadKey();
            }

            if (File.Exists("Connection.config"))
            {
                htConnection = GetSettings("Connection.config");
            }

            oldBgColor = Console.BackgroundColor;
            oldFgColor = Console.ForegroundColor;

            Initialize();

            //string[] strs = { "aaa", "bbb", "ccc", "dd", "eee" };

            //winEmailList.LoadList(strs.ToList());

            if (htConnection!=null)
            {
                tbEmailAddress.Text = htConnection["email"].ToString();
                tbPassword.Text = htConnection["password"].ToString();
                tbServer.Text = htConnection["server"].ToString();
                tbPort.Text = htConnection["port"].ToString();
            }

            Window.GetCurrentWindow().SetToFocus();

            StartLoop();

            Exit();

            //Console.WriteLine("╔═════════════╗");
            //Console.WriteLine("║             ║");
            //Console.WriteLine("╚═════════════╝");

            //Encoding cp437 = Encoding.GetEncoding(437);
            //byte[] source = new byte[1];
            //for (byte i = 0x20; i < 0xFE; i++)
            //{
            //    source[0] = i;
            //    AnsiLookup.Add(i, cp437.GetString(source));
            //}
        }

        public static void Exit()
        {
            Console.BackgroundColor = oldBgColor;
            Console.ForegroundColor = oldFgColor;
            Console.Clear();
            Console.WriteLine("Bye.");
        }

        public static void ReloadMails()
        {
            winEmailList.Draw(); //clear old data
            winEmailBody.Draw(); //clear old data

            DosWindowProgessBar winPB = new DosWindowProgessBar(60, 13, "Retrieving data");
            winPB.SetColors(ConsoleColor.DarkGreen, ConsoleColor.White);
            winPB.Draw(); //show window

            List<string> list = GetMails(winPB); //updates the winPB progress bar

            if (list.Count > 0)
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;

                winEmailList.LoadList(list);

                do //go to Message List (the only of type DosWindowList)
                {
                    Window.GiveFocusToNextWindow();
                }
                while (!(Window.GetCurrentWindow() is DosWindowList));
            }

            System.Threading.Thread.Sleep(150);
            
            winPB.Restore();
        }

        public static List<string> GetMails(DosWindowProgessBar winPB)
        {
            List<string> list=new List<string>();

            //if two-factor authentication is active then it requires app password: https://security.google.com/settings/security/apppasswords

            ImapClient Client = null;
            try
            {
                if (htConnection == null) throw new Exception("Missing connection settings!");
                if (winPB!=null) winPB.SetProgress(2);

                Client = new ImapClient(
                    tbServer.Text, 
                    Convert.ToInt32(tbPort.Text), 
                    tbEmailAddress.Text, 
                    tbPassword.Text,
                    AuthMethod.Login, Convert.ToInt32(tbPort.Text) == 993);

                if (winPB != null) winPB.SetProgress(20);

                IEnumerable<uint> uids = Client.Search(SearchCondition.Unseen());
                if (winPB != null) winPB.SetProgress(40);

                if (uids.Count() != 0)
                {
                    messages = (Client.GetMessages(uids, false)).ToArray();
                    if (winPB != null) winPB.SetProgress(62);

                    list = (from message in messages
                            select message.Subject).ToList();
                }
                else
                {
                    winPB.SetProgress(62);
                    winAbout = new DosWindowMessage("No UNSEEN messages", "Info");
                    winAbout.Draw();
                }
                
            }
            catch (Exception ex)
            {
                string message = ex.Message;

                //check if it is Invalid credentials
                int posIC = message.IndexOf("Invalid credentials!");
                if (posIC != -1)
                    message = message.Substring(posIC);

                DosWindowMessage winError = new DosWindowMessage(25, message, "Error");
                winError.Draw();
            }
            finally
            {
                if (Client != null) Client.Dispose();
            }

            return list;
        }

        public static void CreateTopMenu()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            ScrBuffer.SetCursorPosition(0, 0);

            ScrBuffer.Write("ESC - Quit");
            ScrBuffer.SetCursorPosition(11, 0);
            ScrBuffer.Write("F2 - Check e-mail");
            ScrBuffer.SetCursorPosition(29, 0);
            ScrBuffer.Write("F3 - About");
            ScrBuffer.SetCursorPosition(40, 0);
            ScrBuffer.Write("Tab - switch controls");
        }

        /// <summary>
        /// Used to read a config file such as Connection.config
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static Hashtable GetSettings(string path)
        {
            try
            {
                Hashtable _ret = new Hashtable();
                if (File.Exists(path))
                {
                    StreamReader reader = new StreamReader
                    (
                        new FileStream(
                            path,
                            FileMode.Open,
                            FileAccess.Read,
                            FileShare.Read)
                    );
                    XmlDocument doc = new XmlDocument();
                    string xmlIn = reader.ReadToEnd();
                    reader.Close();
                    doc.LoadXml(xmlIn);
                    foreach (XmlNode child in doc.ChildNodes)
                        if (child.Name.Equals("Settings"))
                            foreach (XmlNode node in child.ChildNodes)
                                if (node.Name.Equals("add"))
                                    _ret.Add
                                    (
                                        node.Attributes["key"].Value,
                                        node.Attributes["value"].Value
                                    );
                }
                return (_ret);
            }
            catch(Exception ex)
            {
                DosWindowMessage winError = new DosWindowMessage("Error loading config file!", "Error");
                winError.Draw();

                return null;
            }
        }
    }
    
}