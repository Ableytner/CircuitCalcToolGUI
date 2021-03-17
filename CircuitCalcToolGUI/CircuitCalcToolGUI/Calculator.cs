using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitCalcToolGUI
{
    class Calculator
    {
        private float U = 0f, I = 0f, Ri = 0f;

        public void setU(float U)
        {
            this.U = U;
        }
        public void setI(float I)
        {
            this.I = I;
        }
        public void setRi(float Ri)
        {
            this.Ri = Ri;
        }

        public float getResult()
        {
            throw new NotImplementedException();
        }
    }
}
