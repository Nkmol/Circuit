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
            Components.Cycle(node =>
            {
                CheckLoop(node);

                node.Next.ForEach(next =>
                {
                    var nextNode = next.Data;
                    nextNode.Inputs.Add(node.Data.Output);
                    nextNode.Calculate();
                });
            });
        }

        public void CheckLoop(GraphNode<Component> node)
        {
            if (node.Previous.Any())
            {
                Components.Cycle(curNode =>
                {
                    if (curNode == node)
                        throw new Exception($"A loop has occured.");

                }, DirectGraph<Component>.Direction.Backwards
                    , node.Previous);
            }
        }
    }
}
