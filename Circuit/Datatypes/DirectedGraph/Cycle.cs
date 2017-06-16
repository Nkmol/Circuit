using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datatypes.DirectedGraph
{
    public class Cycle<T> : List<T>
        where T : GraphNode<T>
    {
        public Cycle(IEnumerable<T> cycle) : base(cycle)
        {
        }

        public string Name { get; set; }
        public int Number { get; set; }
        public T Start { get; set; }
    }
}
