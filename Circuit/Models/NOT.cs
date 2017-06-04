using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class NOT : Port
    {
        public override void Calculate()
        {
            if (Inputs[0] == Bit.HIGH)
            {
                output = Bit.LOW;
            }
            else
            {
                output = Bit.HIGH;
            }
        }
    }
}
