namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DirectGraph<T> : Dictionary<string, T>
        where T : GraphNode
    {
        public List<List<T>> BackwardEdges = new List<List<T>>();

        public bool IsCyclic => BackwardEdges.Count > 0;

        public List<T> First => Values?.Where(x => x.Previous.Count <= 0).ToList();
        public List<T> Lasts => Values?.Where(x => x.Next.Count <= 0).ToList();

        // Returns the each cycle
        public IEnumerable<List<T>> DepthFirstCycle(T start) 
        {
            var stack = new Stack<T>();
            // List to preserve order
            var recursionVisited = new List<T>();

            stack.Push(start);

            while (stack.Count != 0)
            {
                var current = stack.Pop();

                if (recursionVisited.Contains(current))
                {
                    BackwardEdges.Add(new List<T> { recursionVisited.Last(), current });
                    continue;
                }

                recursionVisited.Add(current);

                var nextNeightbours = current.Next.Select(node => node as T).ToList();

                if (nextNeightbours.Count == 0)
                {
                    var cycle = recursionVisited.ToList();

                    // Update visited for back-track
                    recursionVisited.Remove(current); 

                    yield return cycle;
                }
                foreach (var neightbour in nextNeightbours)
                {
                    stack.Push(neightbour);
                }
            }
        }
    }
}