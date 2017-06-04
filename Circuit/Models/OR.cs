using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class OR : Port
    {
        public override void Calculate()
        {
            if (Inputs.Select(x => x == Bit.HIGH).Any())
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
