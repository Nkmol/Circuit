using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class AND : Port
    {
        public override void Calculate()
        {
            if (Inputs.Select(x => x == Bit.HIGH).Count() == Inputs.Count)
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
