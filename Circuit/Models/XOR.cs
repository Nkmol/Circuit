using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class XOR : Port
    {
        public override void Calculate()
        {
            // Odd number HIGH inputs
            if (Inputs.Select(x => x == Bit.HIGH).Count() % 2 != 0)
            {
                output = Bit.HIGH;
            }
            else
            {
                output = Bit.LOW;
            }
        }
    }
}
