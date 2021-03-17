using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitCalcToolGUI
{
    struct Position
    {
        public Position(int X, int Y)
        {
            x = X;
            y = Y;
        }

        public int x;
        public int y;
    }

    class GUI
    {
        private Position cursorPos = new Position(0, 0);

        readonly string top;
        readonly string secTop;
        readonly string bottom;

        public GUI()
        {
            Console.SetWindowSize(60, 30 + 1); // +1 for extra line below last line

            top = ".----------------------------------------------------------.";
            secTop = "|                                                          |";
            bottom = "'----------------------------------------------------------'";

            InitDraw();

            MoveCursor(new Position(1, 1));

            GetUserInput();
        }

        public void MoveCursor(Position pos)
        {
            Console.SetCursorPosition(pos.x, pos.y);
            cursorPos = pos;
            //Console.Write(pos.x); Console.Write(pos.y);
        }

        private void InitDraw()
        {
            Console.WriteLine(top);
            for(int c = 0; c < 28; c++)
            {
                Console.WriteLine(secTop);
            }
            Console.WriteLine(bottom);
        }

        private void GetUserInput()
        {
            bool stop = false;
            
            while (!stop)
            {
                ConsoleKeyInfo key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.Tab:
                        stop = true;
                        break;

                    case ConsoleKey.Enter:
                        throw new NotImplementedException();
                        break;

                    case ConsoleKey.W:
                        Position oldPos = cursorPos;
                        MoveCursor(new Position(oldPos.x, oldPos.y - 1));
                        //Console.Write("T");
                        break;

                    case ConsoleKey.D:
                        oldPos = cursorPos;
                        MoveCursor(new Position(oldPos.x + 1, oldPos.y));
                        break;

                    case ConsoleKey.A:
                        oldPos = cursorPos;
                        MoveCursor(new Position(oldPos.x - 1, oldPos.y));
                        break;

                    default:
                        Console.WriteLine(key.KeyChar);
                        break;
                }
            }
        }
    }
}
