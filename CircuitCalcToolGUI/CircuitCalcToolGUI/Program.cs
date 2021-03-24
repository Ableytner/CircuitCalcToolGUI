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
            gui.AddTextField(new TextField(new Position(5, 5), 15, 1, "Title"));
            gui.AddButton(new Button(new Position(7, 20), 20, "Button"));
            gui.AddButton(new Button(new Position(10, 25), 30, 2, "Button2"));
            gui.AddButton(new Button(new Position(15, 12), 15, 1));
            gui.Start();
        }
    }
}
