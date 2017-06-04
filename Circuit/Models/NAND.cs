using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class NAND : Port
    {
        public override void Calculate()
        {
            if (Inputs.Select(x => x == Bit.HIGH).Count() == Inputs.Count)
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
