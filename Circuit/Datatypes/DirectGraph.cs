using System;
using System.Collections.Generic;

namespace Models
{
    using System.Linq;

    public class DirectGraph<T> : Dictionary<string, GraphNode<T>>
    {
        public List<GraphNode<T>> First
        {
            get
            {
                return this.Values.Where(x => x.Previous.Count <= 0).ToList();
            }
        }

        public void Add(string key, T value)
        {
            Add(key, new GraphNode<T>(value));
        }

        // TODO Improve with stack cycle tracking
        // http://www.geeksforgeeks.org/detect-cycle-in-a-graph/
        public GraphNode<T> LookBack(GraphNode<T> node)
        {
           return ParseLanes(node.Previous, node);
        }

        // TODO add short-circuit parsing (If one is already checked, skip it)
        private GraphNode<T> ParseLanes(List<GraphNode<T>> nodes, GraphNode<T> lookFor)
        {
            if (nodes?.Any() == false || lookFor == null)
                return null;

            foreach (var node in nodes)
            {
                if (node == lookFor)
                    return node;

                if (node.Previous?.Any() == false)
                    continue;

                var result = ParseLanes(node.Previous, lookFor);
                if (result != null)
                    return result;
            }

            return null;
        }
    }
}
