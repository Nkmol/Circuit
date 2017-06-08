namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DirectGraph<T> : Dictionary<string, GraphNode<T>>
    {
        public List<List<GraphNode<T>>> BackwardEdges = new List<List<GraphNode<T>>>();

        public bool IsCyclic => BackwardEdges.Count > 0;

        public List<GraphNode<T>> First => Values?.Where(x => x.Previous.Count <= 0).ToList();
        public List<GraphNode<T>> Lasts => Values?.Where(x => x.Next.Count <= 0).ToList();

        public void Add(string key, T value)
        {
            Add(key, new GraphNode<T>(value));
        }

        // Returns the each cycle
        public IEnumerable<List<GraphNode<T>>> DepthFirstCycle(GraphNode<T> start)
        {
            var stack = new Stack<GraphNode<T>>();
            // List to preserve order
            var recursionVisited = new List<GraphNode<T>>();

            stack.Push(start);

            while (stack.Count != 0)
            {
                var current = stack.Pop();

                if (recursionVisited.Contains(current))
                {
                    BackwardEdges.Add(new List<GraphNode<T>> { recursionVisited.Last(), current });
                    continue;
                }

                recursionVisited.Add(current);

                var nextNeightbours = current.Next;

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