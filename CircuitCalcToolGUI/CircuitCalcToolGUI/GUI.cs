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

        public readonly int x;
        public readonly int y;
    }

    class GUI
    {
        private Position windowSize;
        private string[] content = new string[30];
        private List<TextField> textFields = new List<TextField>();

        private Position cursorPos = new Position(0, 0);
        private int cursorElementPos = 0;

        public GUI(Position windowSize)
        {
            SetWindowSize(windowSize);

            content = StringBuilder.Border(windowSize);

            AddTextField(new TextField(new Position(5, 5), 15, "Title", "+", "+"));
            
            MoveCursor(new Position(5, 5));
        }

        #region Public
        public void Main()
        {
            textFields[0].ChangeValue("TestTe", 0);
            textFields[1].ChangeValue("TestTestTe", 0);

            ImportTextFields();
            RedrawAll();

            GetUserInput();
        }

        public void AddTextField(TextField field)
        {
            textFields.Add(field);
            ImportTextFields();
            RedrawAll();
        }
        #endregion;

        #region OneTime
        private void SetWindowSize(Position windowSize)
        {
            Console.SetWindowSize(windowSize.x, windowSize.y);
            this.windowSize = windowSize;
            Array.Resize(ref content, windowSize.y);
        }
        #endregion

        #region Helpers
        private string ChangeChar(string s, int index, string newChar)
        {
            return s.Remove(index, 1).Insert(index, newChar);
        }
        #endregion

        private void ImportTextFields()
        {
            int x = 0, y = 0;
            foreach(var item in textFields)
            {
                for (int c = item.pos.y - 1; c < item.pos.y + 2; c++)
                {
                    for(int i = item.pos.x - 1; i < ((item.pos.x - 1) + item.width); i++)
                    {
                        content[c] = ChangeChar(content[c], i, item.text[y][x].ToString());
                        x++;
                    }
                    x = 0;
                    y++;
                }
                y = 0;
            }
        }

        private void RedrawAll()
        {
            Console.Clear();

            for(int c = 0; c < windowSize.y - 1; c++)
            {
                Console.WriteLine(content[c]);
            }
            Console.Write(content[windowSize.y - 1]);

            MoveCursor(cursorPos);
        }

        private void MoveCursor(Position pos)
        {
            Console.SetCursorPosition(pos.x, pos.y);
            cursorPos = pos;
        }

        private void TabToNext()
        {
            RedrawAll();

            cursorElementPos++;
            if (cursorElementPos >= textFields.Count)
                cursorElementPos = 0;

            Position newPos = new Position(textFields[cursorElementPos].pos.x + textFields[cursorElementPos].valuePos - 1, textFields[cursorElementPos].pos.y);
            MoveCursor(newPos);
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
                        TabToNext();
                        break;

                    case ConsoleKey.Enter:
                        
                        break;

                    case ConsoleKey.UpArrow:

                        break;

                    case ConsoleKey.DownArrow:

                        break;

                    case ConsoleKey.RightArrow:
                        RedrawAll();

                        MoveCursor(new Position(cursorPos.x + 1, cursorPos.y));
                        break;

                    case ConsoleKey.LeftArrow:
                        RedrawAll();

                        MoveCursor(new Position(cursorPos.x - 1, cursorPos.y));
                        break;

                    default:
                        if (textFields[cursorElementPos].ChangeValue(key.KeyChar, cursorPos.x - textFields[cursorElementPos].pos.x))
                        {
                            ImportTextFields();
                            RedrawAll();

                            MoveCursor(new Position(cursorPos.x + 1, cursorPos.y));
                        }
                        else
                            TabToNext();
                        break;
                }
            }
        }
    }
}
