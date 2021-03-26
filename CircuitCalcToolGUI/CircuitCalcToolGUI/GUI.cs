﻿using System;
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

        private Position windowSize;
        private string[] content;

        public readonly List<TextField> textFields = new List<TextField>();
        public readonly List<Button> buttons = new List<Button>();
        public readonly List<ProtectedTextField> protectedTextFields = new List<ProtectedTextField>();

        private List<Tuple<Position, int[]>> tabPos = new List<Tuple<Position, int[]>>();
        private int tabIndex = 0;

        private Position cursorPos = new Position(0, 0);

        private Tuple<Position, int> background = new Tuple<Position, int>(new Position(), -1);

        #region Public
        public void AddTextField(TextField field)
        {
            textFields.Add(field);
        }
        public void AddButton(Button button)
        {
            buttons.Add(button);
        }
        public void AddProtectedTextField(ProtectedTextField field)
        {
            protectedTextFields.Add(field);
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
            ImportProtectedTextFields();
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
        private void ImportProtectedTextFields()
        {
            int x = 0, y = 0;
            foreach (var item in protectedTextFields)
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

        #region TabPos
        private void SetupTabPos()
        {

            for(int c = 0; c < textFields.Count; c++)
            {
                tabPos.Add(new Tuple<Position, int[]>(new Position(textFields[c].pos.x + textFields[c].valuePos - 1, textFields[c].pos.y), new int[] { 0, c }));
            }
            for(int c = 0; c < buttons.Count; c++)
            {
                tabPos.Add(new Tuple<Position, int[]>(new Position(buttons[c].pos.x, buttons[c].pos.y), new int[] { 1, c }));
            }

            SortTabPos();
        }
        private void SortTabPos()
        {
            int c = 0;
            while (c < tabPos.Count - 1)
            {
                if (tabPos[c].Item1.y > tabPos[c + 1].Item1.y)
                {
                    ExchangeTabPos(c, c + 1);

                    if (c - 1 >= 0)
                        c -= 1;
                }
                else if (tabPos[c].Item1.y == tabPos[c + 1].Item1.y)
                {
                    if (tabPos[c].Item1.x > tabPos[c + 1].Item1.x)
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
            Tuple<Position, int[]> temporary;

            temporary = tabPos[m];
            tabPos[m] = tabPos[n];
            tabPos[n] = temporary;
        }
        #endregion

        #region Tabbing
        private void TabToNext()
        {
            RedrawAll();

            tabIndex++;
            if (tabIndex >= tabPos.Count)
                tabIndex = 0;

            int i = 0;
            if (tabPos[tabIndex].Item2[0] == 0)
            {
                while (i < textFields[tabPos[tabIndex].Item2[1]].value.Length)
                    if (textFields[tabPos[tabIndex].Item2[1]].value[i] != ' ')
                        i++;
                    else
                        break;
            }

            Position newPos = new Position(tabPos[tabIndex].Item1.x + i, tabPos[tabIndex].Item1.y);
            MoveCursor(newPos);
        }
        private void TabToLast()
        {
            RedrawAll();

            tabIndex--;
            if (tabIndex < 0)
                tabIndex = tabPos.Count - 1;

            int i = 0;
            if (tabPos[tabIndex].Item2[0] == 0)
            {
                while (i < textFields[tabPos[tabIndex].Item2[1]].value.Length)
                    if (textFields[tabPos[tabIndex].Item2[1]].value[i] != ' ')
                        i++;
                    else
                        break;
            }

            Position newPos = new Position(tabPos[tabIndex].Item1.x + i, tabPos[tabIndex].Item1.y);
            MoveCursor(newPos);
        }
        #endregion

        #region Helpers
        private string ChangeChar(string s, int index, string newChar)
        {
            return s.Remove(index, 1).Insert(index, newChar);
        }
        #endregion

        private void RedrawAll()
        {
            Console.Clear();
            ImportAll();

            for(int c = 0; c < windowSize.y - 1; c++)
            {
                if(background.Item2 != -1)
                {
                    if (background.Item1.y == c)
                    {
                        string s = "";
                        for (int i = 0; i < background.Item1.x; i++)
                        {
                            s += content[c][i];
                        }
                        Console.Write(s);
                        s = "";

                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;

                        for (int i = 0; i < background.Item2 - 1; i++)
                        {
                            s += content[c][i + background.Item1.x];
                        }
                        Console.Write(s);
                        s = "";

                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;

                        for (int i = background.Item1.x + background.Item2 - 1; i < content[c].Length; i++)
                        {
                            s += content[c][i];
                        }
                        Console.WriteLine(s);
                    }
                    else
                        Console.WriteLine(content[c]);
                }
                else
                    Console.WriteLine(content[c]);
            }
            Console.Write(content[windowSize.y - 1]);
        }

        private void SetBackground()
        {
            if (tabPos.Count > 0)
            {
                background = new Tuple<Position, int>(new Position(), -1);

                if (tabPos[tabIndex].Item2[0] == 1)
                {
                    if (background.Item2 == -1)
                        background = new Tuple<Position, int>(tabPos[tabIndex].Item1, buttons[tabPos[tabIndex].Item2[1]].width - 1);
                }
            }
        }

        private void MoveCursor(Position pos)
        {
            Console.SetCursorPosition(pos.x, pos.y);
            cursorPos = pos;

            SetBackground();

            RedrawAll();
            
            Console.SetCursorPosition(cursorPos.x, cursorPos.y);
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
                        if (tabPos[tabIndex].Item2[0] == 1)
                            buttons[tabPos[tabIndex].Item2[1]].Press();
                        else
                            TabToNext();

                        MoveCursor(cursorPos);
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
                        RedrawAll();
                        MoveCursor(cursorPos);
                        Environment.Exit(0);
                        break;

                    case ConsoleKey.Backspace:
                        if ((tabPos[tabIndex].Item2[0] == 0) && (((cursorPos.x - textFields[tabPos[tabIndex].Item2[1]].pos.x) - textFields[tabPos[tabIndex].Item2[1]].valuePos) >= 0))
                        {
                            if (textFields[tabPos[tabIndex].Item2[1]].ChangeValue(' ', cursorPos.x - textFields[tabPos[tabIndex].Item2[1]].pos.x - 1))
                            {
                                ImportTextFields();
                                RedrawAll();

                                MoveCursor(new Position(cursorPos.x - 1, cursorPos.y));
                            }
                            else
                                TabToNext();
                        }
                        else
                        {
                            RedrawAll();
                            MoveCursor(cursorPos);
                        }
                        break;

                    default:
                        if (tabPos[tabIndex].Item2[0] == 0)
                        {
                            if (textFields[tabPos[tabIndex].Item2[1]].ChangeValue(key.KeyChar, cursorPos.x - textFields[tabPos[tabIndex].Item2[1]].pos.x))
                            {
                                ImportTextFields();
                                RedrawAll();

                                MoveCursor(new Position(cursorPos.x + 1, cursorPos.y));
                            }
                            else
                                TabToNext();
                        }
                        else
                            RedrawAll();
                        break;
                }
            }
        }
    }
}
