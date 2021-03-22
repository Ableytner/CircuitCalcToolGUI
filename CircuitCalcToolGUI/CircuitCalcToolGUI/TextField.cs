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
}
