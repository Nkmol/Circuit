using System;

namespace Views
{
    using System.Collections.Generic;
    using System.Linq;
    using Models;

    public class BoardView : ComponentView
    {
        private Board _board = null;

        public BoardView(Board nodes)
        {
            this._board = nodes;
        }

        // Decorate pattern
        public new void Draw()
        {
            foreach (var node in _board.Components)
            {
                var component = node.Value;

                if (component.Next?.Count > 0)
                {
                    Console.Write($"{component.Data.name} [{component.Data.GetType().Name}, {(int)component.Data.output}]: ");

                    foreach (var nextNode in component.Next)
                    {
                        Console.Write(nextNode.Data.name + " ");
                    }

                    Console.WriteLine();
                }
            }
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
