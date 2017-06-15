using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    using Datatypes;
    using Datatypes.DirectedGraph;

    public abstract class Component : GraphNode
    {
        // TODO Improve super casting from base
        public new List<Component> Next { get; } = new List<Component>();

        public new List<Component> Previous { get; } = new List<Component>();

        public bool IsConnected => Previous.Count > 0 || Next.Count > 0;

        public Bit Value { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public abstract void Calculate();
    }
}
