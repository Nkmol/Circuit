using System;

namespace Views
{
    using Models;

    public class BoardView : ComponentView
    {
        private Board _board = null;
        public BoardView(Board nodes)
        {
            this._board = nodes;
        }

        public void Draw()
        {
            var firstNodes = _board.Components.First;

            foreach (var node in firstNodes)
            {
                ProcessComponent(node);
            }
        }

        private void ProcessComponent(GraphNode<Component> node)
        {
            Console.WriteLine(node.Data.name);

            if (node.Next?.Count <= 0)
                return;

            foreach (var nextNode in node.Next)
            {
                ProcessComponent(nextNode);
            }
        }

        private void DrawComponent()
        {
            
        }
    }
}
