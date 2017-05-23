using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public abstract class Port : Component
    {
        private int delay;

        public Bit CalculateOutput()
        {
            throw new NotImplementedException();
        }
    }
}
