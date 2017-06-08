using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Board
    {
        public DirectGraph<Component> Components { get; }

        public Board(DirectGraph<Component> nodes)
        {
            this.Components = nodes;
        }

        public void Start()
        {
            var firstConnected = Components["Cin"];
            var cyclenr = 0;
            foreach (var cycle in Components.DepthFirstCycle(firstConnected))
            {
                Console.WriteLine($"--- Cycle {cyclenr} ---");
                foreach (var node in cycle)
                {
                    Console.WriteLine("   " + node.Data.Name);
                }
                Console.WriteLine();
                cyclenr++;
            }
        }

        public void WriteLoops()
        {
            foreach (var backwardEdge in Components.BackwardEdges)
            {
                Console.WriteLine($"Contains a loop from {backwardEdge[0].Data.Name} to {backwardEdge[1].Data.Name}");
            }
        }
    }
}
