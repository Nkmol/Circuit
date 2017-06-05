using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Board
    {
        public DirectGraph<Component> Components { get; }

        public Board(DirectGraph<Component> nodes)
        {
            this.Components = nodes;
        }

        public void Start()
        {
            ParseLanes(Components.First);
        }

        private void ParseLanes(List<GraphNode<Component>> lanes)
        {
            foreach (var node in lanes)
            {
                node.Next.ForEach(x => ParseComponent(node, x));

                ParseLanes(node.Next);
            }
        }

        private void ParseComponent(GraphNode<Component> cur, GraphNode<Component> next)
        {
            var nodeCur = cur.Data;
            var nodeNext = next.Data;

            if (Components.LookBack(cur) != null)
            {
                throw new Exception($"A loop has occured. '{nodeCur.name}' to '{nodeNext.name}'");
            }

            nodeNext.Inputs.Add(nodeCur.output);

            nodeNext.Calculate();
        }
    }
}
