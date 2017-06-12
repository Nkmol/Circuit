namespace Views
{
    using System;
    using Models;

    public class BoardView : IDrawAble
    {
        private readonly Board _board;

        public BoardView(Board nodes)
        {
            _board = nodes;
        }

        public void Draw()
        {
            var factory = ComponentViewFactory.Instance;

            foreach (var node in _board.Components)
            {
                var component = node.Value;

                if (component.Next?.Count > 0)
                {
                    factory.Create(component).Draw();
                    Console.Write(": ");

                    foreach (var nextNode in component.Next)
                    {
                        factory.Create(nextNode).Draw();
                        Console.Write(" ");
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