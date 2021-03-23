using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitCalcToolGUI
{
    class TextField
    {
        public TextField(Position pos, int width, string title = "", string cornTop = ".", string cornBot = "'", string borderVert = "|", string borderHori = "-")
        {
            this.pos = pos;
            this.width = width;

            text = StringBuilder.Border(new Position(width, 3), cornTop, cornBot, borderVert, borderHori);
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

            /*Console.WriteLine(text[0]);
            Console.WriteLine(text[1]);
            Console.WriteLine(text[2]);*/
        }

        public readonly Position pos;
        public readonly int width;
        public string[] text = new string[3];
        public string value;
        public int valuePos;

        public bool ChangeValue(char toChange, int index)
        {
            index = index - valuePos + 1;
            if (index < (value.Length - 1))
            {
                value = ChangeChar(value, index, toChange.ToString());
                UpdateArr();

                return true;
            }
            else
                return false;
        }
        public bool ChangeValue(string toChange, int indexStart)
        {
            int i = 0;
            for(int c = indexStart; c < toChange.Length; c++)
            {
                value = ChangeChar(value, c, toChange[i].ToString());
                i++;
            }
            UpdateArr();

            return true;
        }

        private void UpdateArr()
        {
            int i = 0;
            for(int c = valuePos; c < width - 2; c++)
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
