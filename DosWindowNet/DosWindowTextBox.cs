using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DosWindowNet
{
    public class DosWindowTextBox : DosWindow
    {
        int currentOffsetLeft;

        public DosWindowTextBox(int posx, int posy, int width, int height, ConsoleColor bgColor)
            : base(posx, posy, width, height, "")
        {
            base.showBorder = false;

            base.bgColor = bgColor;

            text = new StringBuilder("");
        }

        public override bool ProcessKeyboardEvent(ConsoleKeyInfo ki)
        {
            Console.BackgroundColor = bgColor;

            bool processed = false;

            //if we after the text we do not have anything to delete so we just move left
            if (ki.Key == ConsoleKey.Backspace && currentOffsetLeft > text.Length)
                ki = new ConsoleKeyInfo('\0', ConsoleKey.LeftArrow, false, false, false);

            //backspace
            if (ki.Key == ConsoleKey.Backspace && (currentOffsetLeft - 1) >= 0)
            {
                Console.SetCursorPosition(posx, posy + 1);

                text.Remove(currentOffsetLeft - 1, 1);

                Console.Write(text + " ");

                currentOffsetLeft = currentOffsetLeft - 1;

                Console.SetCursorPosition(posx + currentOffsetLeft, posy + 1);


                processed = true;
            }
            else
            //move right
            if (ki.Key == ConsoleKey.RightArrow)
            {
                if ((currentOffsetLeft + 1) < base.width)
                {
                    currentOffsetLeft = currentOffsetLeft + 1;
                    Console.SetCursorPosition(posx + currentOffsetLeft, posy + 1);
                }
                processed = true;
            }
            else
            //move left
            if (ki.Key == ConsoleKey.LeftArrow && (currentOffsetLeft - 1) >= 0)
            {
                currentOffsetLeft = currentOffsetLeft - 1;
                Console.SetCursorPosition(posx + currentOffsetLeft, posy + 1);
                processed = true;
            }
            else //type on screen
            if (currentOffsetLeft < base.width)
            {
                //verify allowed characters
                if (char.IsLetterOrDigit(ki.KeyChar) || char.IsPunctuation(ki.KeyChar) || char.IsWhiteSpace(ki.KeyChar) && ki.Key != ConsoleKey.Tab)
                {
                    if (currentOffsetLeft < text.Length)
                    {
                        text.Insert(currentOffsetLeft, ki.KeyChar);
                        Console.SetCursorPosition(posx, posy + 1);
                        Console.Write(text);
                        Console.SetCursorPosition(posx + currentOffsetLeft, posy + 1);
                    }
                    else //append
                    {
                        text.Append(ki.KeyChar);
                        Console.Write(ki.KeyChar);
                    }

                    currentOffsetLeft = currentOffsetLeft + 1;
                    processed = true;
                }
            }

            return processed;
        }

        public override string Text
        {
            get
            {
                return text.ToString();
            }
            set
            {
                Console.BackgroundColor = bgColor;
                Console.SetCursorPosition(posx, posy + 1);
                Console.Write(value);
                text = new StringBuilder(value);
                currentOffsetLeft = 0;
            }
        }

        public override void SetToFocus()
        {
            base.SetToFocus();
            currentOffsetLeft = 0;
        }
    }
}
