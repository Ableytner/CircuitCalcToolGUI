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
            //Testing();
            CalculatorInit();
        }

        static void CalculatorInit()
        {
            GUI gui = new GUI(new Position(47, 20), 1);
            TextField[] fields = new TextField[3];
            fields[0] = new TextField(new Position(2, 2), 15, "U");
            fields[1] = new TextField(new Position(17, 2), 15, "I");
            fields[2] = new TextField(new Position(32, 2), 15, "Ri");

            Button calculate = new Button(new Position(17, 5), 15, "Calculate");
            ProtectedTextField result = new ProtectedTextField(new Position(17, 8), 15);

            foreach (var item in fields)
                gui.AddTextField(item);
            gui.AddButton(calculate);
            gui.AddProtectedTextField(result);

            gui.Start();
        }

        static void Testing()
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

            ProtectedTextField field = new ProtectedTextField(new Position(40, 20), 15, "Field1");
            gui.AddProtectedTextField(field);
            field.ChangeValue('2', 5);
            field.ChangeValue('3', 5);

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
