using System.Collections.Generic;
using Datatypes.DirectedGraph;
using Helpers.DGML;

namespace Models
{
    public class BoardDgmlParser : DGMLParser
    {
        public List<Edge<Component>> ParseCyclesToEdge(Cycle<Component> cycle)
        {
            var links = new List<Edge<Component>>();
            Component prev = null;

            foreach (var node in cycle)
            {
                if (prev != null)
                    links.Add(new Edge<Component>(prev, node));

                prev = node;
            }

            return links;
        }
    }
}