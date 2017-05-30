using System;
using System.Collections.Generic;

namespace Models
{
    public class DirectGraph<T> : Dictionary<string, GraphNode<T>>
    {
        public void Add(string key, T value)
        {
            Add(key, new GraphNode<T>(value));
        }
    }
}
