namespace Models
{
    using System.Collections.Generic;

    public class GraphNode<T>
    {
        public List<GraphNode<T>> Next;
        public List<GraphNode<T>> Previous;

        public GraphNode(T value)
        {
            Next = new List<GraphNode<T>>();
            Previous = new List<GraphNode<T>>();
            Data = value;
        }

        public T Data { get; set; }

        public void LinkNext(GraphNode<T> value)
        {
            Next.Add(value);
            value.Previous.Add(this);
        }
    }
}