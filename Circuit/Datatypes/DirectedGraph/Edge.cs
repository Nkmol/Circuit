namespace Datatypes.DirectedGraph
{
    // Edge is a relation between 2 nodes
    public class Edge<T>
        where T : class
    {
        public T From { get; }
        public T To { get; }

        public Edge(T from, T to = null)
        {
            From = from;
            To = to;
        }
    }
}