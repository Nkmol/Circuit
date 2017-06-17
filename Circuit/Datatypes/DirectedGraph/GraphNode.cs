using System.Collections.Generic;

namespace Datatypes.DirectedGraph
{
    public class GraphNode<T>
        where T : GraphNode<T>
    {
        public string Name;
        public List<T> Next;
        public List<T> Previous;

        public GraphNode()
        {
            Next = new List<T>();
            Previous = new List<T>();
        }

        public void LinkNext(T value)
        {
            Next.Add(value);
            value.Previous.Add((T) this);
        }
    }
}