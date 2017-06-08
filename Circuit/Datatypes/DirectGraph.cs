using Datatypes;

namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DirectGraph<T> : Dictionary<string, T>
        where T : GraphNode
    {
        // https://en.wikipedia.org/wiki/Depth-first_search#Output_of_a_depth-first_search
        public List<Edge<T>> BackEdges = new List<Edge<T>>();

        public bool IsCyclic => BackEdges.Count > 0;

        public bool EdgeExists(string from, string to)
        {
            return this[from].Next.Contains(this[to]);
        }

        // Returns the each cycle
        public IEnumerable<List<T>> DepthFirstCycle(T start) 
        {
            var stack = new Stack<Edge<T>>();
            // List to preserve order
            var recursionVisited = new List<T>();

            stack.Push(new Edge<T>(start));

            while (stack.Count != 0)
            {
                var currentEdge = stack.Pop();
                var current = currentEdge.To ?? currentEdge.From;

                if (recursionVisited.Contains(current))
                {
                    BackEdges.Add(currentEdge);

                    // Update visited for back-track
                    // Move as last visited
                    

                    continue;
                }

                recursionVisited.Add(current);

                var nextNeightbours = current.Next.Select(node => node as T).ToList();

                // Only returns valid cycles
                if (nextNeightbours.Count == 0)
                {
                    var cycle = recursionVisited.ToList();

                    // Update visited for back-track if graph has not completed
                    if (stack.Count > 0)
                    {
                        var index = recursionVisited.IndexOf(stack.Peek().From);
                        recursionVisited.RemoveRange(index + 1, recursionVisited.Count - (index + 1));
                    }

                    yield return cycle;
                }

                foreach (var neightbour in nextNeightbours)
                {
                    stack.Push(new Edge<T>(current, neightbour));
                }
            }
        }
    }
}