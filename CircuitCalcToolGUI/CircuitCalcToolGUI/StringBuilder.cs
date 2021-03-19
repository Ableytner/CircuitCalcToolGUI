using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitCalcToolGUI
{
    class StringBuilder
    {
        public static string[] Border(Position size, string cornTop = ".", string cornBot = "'", string borderVert = "|", string borderHori = "-")
        {
            string[] border = new string[size.y];

            border[0] += cornTop;
            for (int c = 1; c < size.x - 1; c++)
                border[0] += borderHori;
            border[0] += cornTop;

            for(int i = 1; i < size.y - 1; i++)
            {
                border[i] += borderVert;
                for (int c = 1; c < size.x - 1; c++)
                    border[i] += " ";
                border[i] += borderVert;
            }

            border[size.y - 1] += cornBot;
            for (int c = 1; c < size.x - 1; c++)
                border[size.y - 1] += borderHori;
            border[size.y - 1] += cornBot;

            return border;
        }
    }
}
