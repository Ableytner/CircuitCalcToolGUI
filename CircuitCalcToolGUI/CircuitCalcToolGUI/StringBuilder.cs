using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitCalcToolGUI
{
    struct Border
    {
        public Border(Position size, string cornTop = ".", string cornBot = "'", string borderVert = "|", string borderHori = "-")
        {
            this.size = size;
            this.cornTop = cornTop;
            this.cornBot = cornBot;
            this.borderVert = borderVert;
            this.borderHori = borderHori;
        }

        public readonly Position size;
        public readonly string cornTop;
        public readonly string cornBot;
        public readonly string borderVert;
        public readonly string borderHori;
    }

    class StringBuilder
    {
        public static string[] Border(Position size)
        {
            return DrawBorder(GetPreset(size, 0));
        }
        public static string[] Border(Border border)
        {
            return DrawBorder(border);
        }

        private static string[] DrawBorder(Border borderPreset)
        {
            string[] border = new string[borderPreset.size.y];

            border[0] += borderPreset.cornTop;
            for (int c = 1; c < borderPreset.size.x - 1; c++)
                border[0] += borderPreset.borderHori;
            border[0] += borderPreset.cornTop;

            for (int i = 1; i < borderPreset.size.y - 1; i++)
            {
                border[i] += borderPreset.borderVert;
                for (int c = 1; c < borderPreset.size.x - 1; c++)
                    border[i] += " ";
                border[i] += borderPreset.borderVert;
            }

            border[borderPreset.size.y - 1] += borderPreset.cornBot;
            for (int c = 1; c < borderPreset.size.x - 1; c++)
                border[borderPreset.size.y - 1] += borderPreset.borderHori;
            border[borderPreset.size.y - 1] += borderPreset.cornBot;

            return border;
        }

        public static Border GetPreset(Position size, int preset)
        {
            switch (preset)
            {
                case 0:
                    return new Border(size, ".", "'", "|", "-");

                case 1:
                    return new Border(size, "+", "+", "|", "-");

                case 2:
                    return new Border(size, " ", " ", " ", " ");

                default:
                    return GetPreset(size, 0);
            }
        }
    }
}
