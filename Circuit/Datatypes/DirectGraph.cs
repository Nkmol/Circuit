namespace Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class DirectGraph<T> : Dictionary<string, GraphNode<T>>
    {
        public List<GraphNode<T>> First
        {
            get { return Values.Where(x => x.Previous.Count <= 0).ToList(); }
        }

        public void Add(string key, T value)
        {
            Add(key, new GraphNode<T>(value));
        }
    }
}