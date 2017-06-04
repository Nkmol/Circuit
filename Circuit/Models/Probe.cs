using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Probe : Component
    {
        public override void Calculate()
        {
            output = Inputs.FirstOrDefault();
        }
    }
}
