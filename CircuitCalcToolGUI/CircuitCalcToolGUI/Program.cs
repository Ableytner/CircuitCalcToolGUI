using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitCalcToolGUI
{
    class Program
    {
        static void Main(string[] args)
        {
            GUI gui = new GUI(new Position(60, 30));
            gui.AddTextField(new TextField(new Position(7, 20), 20, "Title2"));
            gui.Main();
        }
    }
}
