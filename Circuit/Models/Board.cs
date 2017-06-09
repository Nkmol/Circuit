using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Board : Component
    {
        public DirectGraph<Component> Components { get; }

        public Board(DirectGraph<Component> nodes)
        {
            this.Components = nodes;
        }

        // Multiple outputs represent multiple values
        // TODO Improve so fits better with composite patern
        // Default atleast one output
        public new Bit[] Value { get; private set; }  = { Bit.LOW };

        // No input is connected or no output is connected
        public new bool IsConnected { get; set; } = true;

        public override void Calculate()
        {
            if (Start())
            {
                Value = Components
                    .Select(pair => pair.Value)
                    .Where(node => node is PROBE && node.IsConnected)
                    .Select(node => node.Value)
                    .ToArray();
            }
        }

        public bool IsCyclic => Components.IsCyclic;

        private bool Start()
        {
            if (CheckConnection())
            {
                var inputs = Components.Select(pair => pair.Value).Where(node => node is INPUT && node.IsConnected);

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
            var firstInput = Components.Select(pair => pair.Value).FirstOrDefault(node => node is INPUT && node.IsConnected);
            var firstProbe = Components.FirstOrDefault(node => node.Value is PROBE).Value;

            if (firstInput == null || firstProbe == null)
            {
                Console.WriteLine($"This board is not connected");
                IsConnected = false;
            }

            return IsConnected;
        }
    }
}
