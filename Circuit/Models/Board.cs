using System.Collections.Generic;
using System.Linq;
using Datatypes.DirectedGraph;

namespace Models
{
    public class Board : Component
    {
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

        public DirectGraph<Component> Components { get; }
        public bool IsCyclic => Components.IsCyclic;
        public List<Component> Probes => Components.Select(x => x.Value).Where(x => x is Probe).ToList();

        // No input is connected or no output is connected
        public new bool IsConnected { get; set; } = true;

        public override void Calculate()
        {
            // TODO Support multiplle Input + Output for sub-boards
            var firstOrDefault = Previous.FirstOrDefault(x => x.Value == Bit.HIGH);
            if (firstOrDefault != null)
                Value = firstOrDefault.Value;

            foreach (var cycle in Cycle())
                cycle.ForEach(x => x.Calculate());
        }

        public IEnumerable<Cycle<Component>> Cycle()
        {
            var inputs = Components.Select(pair => pair.Value).Where(node => node is Input && node.IsConnected);

            // Depth first search for every input
            foreach (var input in inputs)
                // TODO Able to call without yield
            foreach (var cycle in Components.DepthFirstCycle(input))
            {
                cycle.Name = $"Cycle {Name}";

                yield return cycle;
            }
        }

        public bool CheckConnection()
        {
            var firstInput = Components.Select(pair => pair.Value)
                .FirstOrDefault(node => node is Input && node.IsConnected);
            var firstProbe = Components.FirstOrDefault(node => node.Value is Probe).Value;

            if (firstInput == null || firstProbe == null)
                IsConnected = false;

            return IsConnected;
        }
    }
}