namespace Datatypes.DirectedGraph
{
    using System.Collections.Generic;

    public class GraphNode
    {
        public List<GraphNode> Next;
        public List<GraphNode> Previous;

        public GraphNode()
        {
            Next = new List<GraphNode>();
            Previous = new List<GraphNode>();
        }

        public void LinkNext(GraphNode value)
        {
            Next.Add(value);
            value.Previous.Add(this);
        }
    }
}