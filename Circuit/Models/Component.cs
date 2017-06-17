using Datatypes.DirectedGraph;

namespace Models
{
    public abstract class Component : GraphNode<Component>
    {
        public bool IsConnected => Previous.Count > 0 || Next.Count > 0;

        public Bit Value { get; set; }
        public string Description { get; set; }

        public abstract void Calculate();
    }
}