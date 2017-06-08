using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public abstract class Component : GraphNode
    {
        // TODO Improve super casting from base
        public new List<Component> Next
        {
            get { return base.Next.Select(node => node as Component).ToList(); }
        }

        public new List<Component> Previous;

        public Bit Value { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public abstract void Calculate();
    }
}
