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
        private readonly List<TextField> textFields = new List<TextField>();
        private readonly List<Button> buttons = new List<Button>();

        private List<Position> tabPos = new List<Position>();
        private int tabIndex = 0;
        private Position cursorPos = new Position(0, 0);
        private int cursorElementPos = 0;

        public GUI(Position windowSize)
        {
            SetWindowSize(windowSize);

            content = StringBuilder.Border(StringBuilder.GetPreset(windowSize, 0));
        }
        public GUI(Position windowSize, int borderPreset)
        {
            SetWindowSize(windowSize);

            content = StringBuilder.Border(StringBuilder.GetPreset(windowSize, borderPreset));
        }

        #region Public
        public void AddTextField(TextField field)
        {
            textFields.Add(field);
        }
        public void AddButton(Button button)
        {
            buttons.Add(button);
        }
        #endregion;

        #region OneTime
        private void SetWindowSize(Position windowSize)
        {
            Console.SetWindowSize(windowSize.x, windowSize.y);
            this.windowSize = windowSize;
            Array.Resize(ref content, windowSize.y);
        }
        public void Start()
        {
            /*textFields[0].ChangeValue("TestTe", 0);
            textFields[1].ChangeValue("TestTestTe", 0);*/

            SetupTabPos();

            ImportAll();
            RedrawAll();

            if (tabPos.Count >= 1)
            {
                tabIndex = tabPos.Count;
                TabToNext();

                GetUserInput();
            }
            else
                MoveCursor(new Position(1, 1));
        }
        #endregion

        #region Importers
        private void ImportAll()
        {
            ImportTextFields();
            ImportButtons();
        }
        private void ImportTextFields()
        {
            int x = 0, y = 0;
            foreach (var item in textFields)
            {
                for (int c = item.pos.y - 1; c < item.pos.y + 2; c++)
                {
                    for (int i = item.pos.x - 1; i < ((item.pos.x - 1) + item.width); i++)
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
        private void ImportButtons()
        {
            int x = 0, y = 0;
            foreach (var item in buttons)
            {
                for (int c = item.pos.y - 1; c < item.pos.y + 2; c++)
                {
                    for (int i = item.pos.x - 1; i < ((item.pos.x - 1) + item.width); i++)
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
        #endregion

        #region Helpers
        private string ChangeChar(string s, int index, string newChar)
        {
            return s.Remove(index, 1).Insert(index, newChar);
        }
        #endregion

        #region TabPos
        private void SetupTabPos()
        {
            foreach(var item in textFields)
            {
                tabPos.Add(new Position(item.pos.x + item.valuePos - 1, item.pos.y));
            }
            foreach(var item in buttons)
            {
                tabPos.Add(new Position(item.pos.x + item.titlePos - 1, item.pos.y));
            }

            SortTabPos();
        }
        private void SortTabPos()
        {
            int c = 0;
            while (c < tabPos.Count - 1)
            {
                if (tabPos[c].y > tabPos[c + 1].y)
                {
                    ExchangeTabPos(c, c + 1);

                    if (c - 1 >= 0)
                        c -= 1;
                }
                else if (tabPos[c].y == tabPos[c + 1].y)
                {
                    if (tabPos[c].x > tabPos[c + 1].x)
                    {
                        ExchangeTabPos(c, c + 1);

                        if (c - 1 >= 0)
                            c -= 1;
                    }
                    else
                        c++;
                }
                else
                    c++;
            }
        }
        public void ExchangeTabPos(int m, int n)
        {
            Position temporary;

            temporary = tabPos[m];
            tabPos[m] = tabPos[n];
            tabPos[n] = temporary;
        }
        #endregion

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

            tabIndex++;
            if (tabIndex >= tabPos.Count)
                tabIndex = 0;

            Position newPos = new Position(tabPos[tabIndex].x, tabPos[tabIndex].y);
            MoveCursor(newPos);

            /*cursorElementPos++;
            if (cursorElementPos >= textFields.Count)
                cursorElementPos = 0;

            newPos = new Position(textFields[cursorElementPos].pos.x + textFields[cursorElementPos].valuePos - 1, textFields[cursorElementPos].pos.y);
            MoveCursor(newPos);*/
        }
        private void TabToLast()
        {
            RedrawAll();

            tabIndex--;
            if (tabIndex < 0)
                tabIndex = tabPos.Count - 1;

            Position newPos = new Position(tabPos[tabIndex].x, tabPos[tabIndex].y);
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
                        TabToLast();
                        break;

                    case ConsoleKey.DownArrow:
                        TabToNext();
                        break;

                    case ConsoleKey.RightArrow:
                        RedrawAll();

                        MoveCursor(new Position(cursorPos.x + 1, cursorPos.y));
                        break;

                    case ConsoleKey.LeftArrow:
                        RedrawAll();

                        MoveCursor(new Position(cursorPos.x - 1, cursorPos.y));
                        break;

                    case ConsoleKey.Escape:
                        Environment.Exit(0);
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
