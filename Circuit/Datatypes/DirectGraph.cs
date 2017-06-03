using System;
using System.Collections.Generic;

namespace Models
{
    using System.Linq;

    public class DirectGraph<T> : Dictionary<string, GraphNode<T>>
    {
        public List<GraphNode<T>> First
        {
            get
            {
                return this.Values.Where(x => x.Previous.Count <= 0).ToList();
            }
        }

        public void Add(string key, T value)
        {
            Add(key, new GraphNode<T>(value));
        }
    }
}
