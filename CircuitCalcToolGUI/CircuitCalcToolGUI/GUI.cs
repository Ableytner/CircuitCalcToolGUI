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

    class TextField
    {
        public TextField(Position pos, int width, string title = "", string cornTop = ".", string cornBot = "'", string borderVert = "|", string borderHori = "-")
        {
            this.pos = pos;
            this.width = width;

            text = StringBuilder.Border(new Position(width, 3), cornTop, cornBot, borderVert, borderHori);

            if (!(title == "" || title == null))
            {
                text[1] = borderVert + title + ": ";
                for (int c = 0; c < (width - title.Length - 4); c++)
                {
                    text[1] += " ";
                }
                text[1] += borderVert;
            }

            /*Console.WriteLine(text[0]);
            Console.WriteLine(text[1]);
            Console.WriteLine(text[2]);*/
        }

        public readonly Position pos;
        public readonly int width;
        public string[] text = new string[3];

    }

    class GUI
    {
        private Position windowSize;
        private string[] content = new string[30];
        private List<TextField> textFields = new List<TextField>();

        private Position cursorPos = new Position(0, 0);

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
                    for(int i = item.pos.x - 1; i < (item.pos.x - 1) + item.width; i++)
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
        }

        private void MoveCursor(Position pos)
        {
            Console.SetCursorPosition(pos.x, pos.y);
            cursorPos = pos;
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
                        
                        break;

                    case ConsoleKey.Enter:
                        
                        break;

                    case ConsoleKey.UpArrow:

                        break;

                    case ConsoleKey.DownArrow:

                        break;

                    case ConsoleKey.RightArrow:

                        break;

                    case ConsoleKey.LeftArrow:

                        break;

                    default:
                        
                        break;
                }
            }
        }
    }
}
