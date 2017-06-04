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
                node.Next.ForEach(x => ParseComponent(node.Data, x.Data));

                ParseLanes(node.Next);
            }
        }

        private void ParseComponent(Component cur, Component next)
        {
            next.Inputs.Add(cur.output);

            next.Calculate();
        }
    }
}
