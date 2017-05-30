using System;
using System.Collections.Generic;

namespace Models
{
    public class DirectGraph : Dictionary<string, GraphNode>
    {
        public void Add(string key, Component value)
        {
            Add(key, new GraphNode(value));
        }
    }
}
