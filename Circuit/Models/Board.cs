namespace Models
{
    public class Board
    {
        public Board(DirectGraph<Component> nodes)
        {
            Components = nodes;
        }

        public DirectGraph<Component> Components { get; }
    }
}