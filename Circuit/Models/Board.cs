using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;

namespace Models
{
    using Datatypes;
    using Datatypes.DirectedGraph;

    public class Board : Component
    {
        public DirectGraph<Component> Components { get; }


        public Board()
        {
            Components = new DirectGraph<Component>();

            // Default board name
            Name = "RootBoard";
        }

        public Board(DirectGraph<Component> nodes) : this()
        {
            Components = nodes;
        }

        // No input is connected or no output is connected
        public new bool IsConnected { get; set; } = true;

        public override void Calculate()
        {
            // TODO Support multiplle Input + Output for sub-boards
            if (Start())
            {
                var firstOrDefault = Previous.FirstOrDefault(x => x.Value == Bit.HIGH);
                if (firstOrDefault != null)
                    Value = firstOrDefault.Value;
            }
        }

        public bool IsCyclic => Components.IsCyclic;

        private bool Start()
        {
            if (CheckConnection())
            {
                var inputs = Components.Select(pair => pair.Value).Where(node => node is Input && node.IsConnected);

                // Depth first search for every input
                var cyclenr = 0;
                foreach (var input in inputs)
                {
                    // TODO Able to call without yield
                    foreach (var cycle in Components.DepthFirstCycle(input))
                    {
                        Console.WriteLine($"--- Cycle {cyclenr} ---");
                        foreach (var node in cycle)
                        {
                            Console.WriteLine("   " + node.Name);
                        }
                        Console.WriteLine();
                        cyclenr++;
                    }
                }

                if (CheckLoop())
                {
                    foreach (var node in Components.Values)
                    {
                        node.Calculate();
                    }

                    return true;
                }
            }

            return false;
        }

        public bool CheckLoop()
        {
            if (IsCyclic)
            {
                foreach (var backwardEdge in Components.BackEdges)
                {
                    Console.WriteLine($"Contains a loop from {backwardEdge.From.Name} to {backwardEdge.To.Name}");
                }
            }

            return !IsCyclic;
        }

        public bool CheckConnection()
        {
            var firstInput = Components.Select(pair => pair.Value).FirstOrDefault(node => node is Input && node.IsConnected);
            var firstProbe = Components.FirstOrDefault(node => node.Value is Probe).Value;

            if (firstInput == null || firstProbe == null)
            {
                Console.WriteLine($"This board is not connected");
                IsConnected = false;
            }

            return IsConnected;
        }
    }
}
