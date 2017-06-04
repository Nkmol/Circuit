using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public abstract class Component
    {
        public string name;
        public string description;
        public Bit output = Bit.LOW;

        public List<Bit> Inputs { get; } = new List<Bit>();

        public abstract void Calculate();
    }
}
