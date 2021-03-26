using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitCalcToolGUI
{
    class Calculator
    {
        private double U = 0d, I = 0d, Ri = 0d;

        public void setU(double U)
        {
            this.U = U;
        }
        public void setI(double I)
        {
            this.I = I;
        }
        public void setRi(double Ri)
        {
            this.Ri = Ri;
        }

        public double getResult()
        {
            return 1.234d;
        }
    }
}
