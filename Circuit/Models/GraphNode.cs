using System;
using System.Collections.Generic;

namespace Models
{
    public class GraphNode
    {
		public List<GraphNode> Next;
        public List<GraphNode> Previous;

		public Component Data { get; set; }

        public GraphNode(Component value) {
            Next = new List<GraphNode>();
            Previous = new List<GraphNode>();
            this.Data = value;
        }

    }
}
