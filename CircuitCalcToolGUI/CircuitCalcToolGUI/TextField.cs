using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitCalcToolGUI
{
    class TextField
    {
        public TextField(Position pos, int width, string title = "")
        {
            this.pos = pos;
            this.width = width;
            this.title = title;

            Border border = StringBuilder.GetPreset(new Position(width, 3), 0);
            text = StringBuilder.Border(border);

            Initialize(border.borderVert);

        }
        public TextField(Position pos, int width, int preset, string title = "")
        {
            this.pos = pos;
            this.width = width;
            this.title = title;

            Border border = StringBuilder.GetPreset(new Position(width, 3), preset);
            text = StringBuilder.Border(border);

            Initialize(border.borderVert);

        }
        public TextField(Position pos, int width, string cornTop, string cornBot, string borderVert, string borderHori, string title = "")
        {
            this.pos = pos;
            this.width = width;
            this.title = title;

            text = StringBuilder.Border(new Border(new Position(width, 3), cornTop, cornBot, borderVert, borderHori));

            Initialize(borderVert);
        }

        private void Initialize(string borderVert)
        {
            value = "";

            if (!(title == "" || title == null))
            {
                text[1] = borderVert + title + ": ";
                for (int c = 0; c < (width - title.Length - 4); c++)
                {
                    text[1] += " ";
                    value += " ";
                }
                text[1] += borderVert;
                valuePos = title.Length + 3;

            }
            else
            {
                valuePos = 1;
                for (int c = 0; c < width - 2; c++)
                    value += " ";
            }
        }

        public readonly Position pos;
        public readonly int width;
        public string[] text = new string[3];
        public string value;
        public int valuePos;
        private string title;

        public bool ChangeValue(char toChange, int index)
        {
            index = index - valuePos + 1;
            if (index < (value.Length))
            {
                value = ChangeChar(value, index, toChange.ToString());
                UpdateArr();

                return true;
            }
            else
                return false;
        }

        private void UpdateArr()
        {
            int i = 0;
            for(int c = valuePos; c < width - 1; c++)
            {
                text[1] = ChangeChar(text[1], c, value[i].ToString());
                if (i < value.Length)
                    i++;
                else
                    break;
            }
        }

        private string ChangeChar(string s, int index, string newChar)
        {
            return s.Remove(index, 1).Insert(index, newChar);
        }
    }
}
