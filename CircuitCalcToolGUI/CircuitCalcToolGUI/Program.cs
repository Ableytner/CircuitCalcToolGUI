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

            Button button = new Button(new Position(7, 20), 20, "Button");
            button.Clicked += Button1Clicked;
            gui.AddButton(button);

            button = new Button(new Position(10, 25), 30, 2, "Button2");
            button.Clicked += Button2Clicked;
            gui.AddButton(button);

            button = new Button(new Position(25, 5), 15, 1, "Button3");
            button.Clicked += Button3Clicked;
            gui.AddButton(button);

            button = new Button(new Position(15, 15), 15, 1, "Button4");
            button.Clicked += Button4Clicked;
            gui.AddButton(button);

            gui.Start();
        }

        static void Button1Clicked(object sender, EventArgs args)
        {
            throw new Exception("Yay!");
        }
        static void Button2Clicked(object sender, EventArgs args)
        {
            throw new Exception("Yay2!");
        }
        static void Button3Clicked(object sender, EventArgs args)
        {
            throw new Exception("Yay3!");
        }
        static void Button4Clicked(object sender, EventArgs args)
        {
            throw new Exception("Yay4!");
        }
    }
}
