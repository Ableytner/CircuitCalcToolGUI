﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitCalcToolGUI
{
    class ProtectedTextField
    {
        public ProtectedTextField(Position pos, int width, string value = "")
        {
            this.pos = pos;
            this.width = width;
            this.value = value;

            Border border = StringBuilder.GetPreset(new Position(width, 3), 0);
            text = StringBuilder.Border(border);

            Initialize(border.borderVert);

        }
        public ProtectedTextField(Position pos, int width, int preset, string value = "")
        {
            this.pos = pos;
            this.width = width;
            this.value = value;

            Border border = StringBuilder.GetPreset(new Position(width, 3), preset);
            text = StringBuilder.Border(border);

            Initialize(border.borderVert);

        }
        public ProtectedTextField(Position pos, int width, string cornTop, string cornBot, string borderVert, string borderHori, string value = "")
        {
            this.pos = pos;
            this.width = width;
            this.value = value;

            text = StringBuilder.Border(new Border(new Position(width, 3), cornTop, cornBot, borderVert, borderHori));

            Initialize(borderVert);
        }

        private void Initialize(string borderVert)
        {
            text[1] = borderVert + value;
            for (int c = 0; c < (width - value.Length - 2); c++)
            {
                text[1] += " ";
            }
            text[1] += borderVert;
        }

        public readonly Position pos;
        public readonly int width;
        public string[] text = new string[3];
        public string value;

        public bool ChangeValue(char toChange, int index)
        {
            if (!(index < value.Length))
            {
                value += " ";
                value = ChangeChar(value, index, toChange.ToString());
                UpdateArr();

                return true;
            }
            else
                return false;
        }
        public bool ChangeValue(string toChange)
        {
            value = toChange;
            UpdateArr();
            return true;
        }

        private void UpdateArr()
        {
            for (int c = 0; c < width - 1; c++)
            {
                if(c < value.Length)
                    text[1] = ChangeChar(text[1], c + 1, value[c].ToString());
                else
                    text[1] = ChangeChar(text[1], c + 1, " ");
            }
        }

        private string ChangeChar(string s, int index, string newChar)
        {
            return s.Remove(index, 1).Insert(index, newChar);
        }
    }
}
