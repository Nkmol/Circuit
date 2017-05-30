using System;
using System.Collections.Generic;

namespace Models
{
    public class GraphNode<T>
    {
		public List<GraphNode<T>> Next;
        public List<GraphNode<T>> Previous;

		public T Data { get; set; }

        public GraphNode(T value) {
            Next = new List<GraphNode<T>>();
            Previous = new List<GraphNode<T>>();
            this.Data = value;
        }

        public void LinkNext(GraphNode<T> value)
        {
            Next.Add(value);
            value.Previous.Add(this);
        }
    }
}
