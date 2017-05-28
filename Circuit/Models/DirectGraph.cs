using System;
using System.Collections.Generic;

namespace Models
{
    public class DirectGraph
    {
        private List<GraphNode> nodeSet;

        public DirectGraph(){
            this.nodeSet = new List<GraphNode>();
        }

		public void AddNode(GraphNode node)
		{
			// adds a node to the graph
			nodeSet.Add(node);
		}

        public bool Contains(GraphNode value)
		{
            return nodeSet.Find(n => n.Data.name == value.Data.name) != null;
		}

        public GraphNode GetNode(GraphNode value){
            return nodeSet.Find(n => n.Data.name == value.Data.name);
        }

        public GraphNode GetFirst(){
            return this.nodeSet[0];
        }

        public List<GraphNode> Nodes
		{
			get
			{
				return nodeSet;
			}
		}

		public int Count
		{
			get { return nodeSet.Count; }
		}

    }
}
