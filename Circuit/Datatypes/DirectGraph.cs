namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DirectGraph<T> : Dictionary<string, GraphNode<T>>
    {
        public enum Direction
        {
            Backwards,
            Forwards
        }

        public List<GraphNode<T>> First => Values?.Where(x => x.Previous.Count <= 0).ToList();
        public List<GraphNode<T>> Lasts => Values?.Where(x => x.Next.Count <= 0).ToList();

        public void Add(string key, T value)
        {
            Add(key, new GraphNode<T>(value));
        }

        // TODO Add forward and backwards strategy
        public void Cycle(Action<GraphNode<T>> componentParser, Direction direction = Direction.Forwards, List<GraphNode<T>> startingPoint = null)
        {
            if (startingPoint == null || !startingPoint.Any())
            {
                if (direction == Direction.Forwards)
                {
                    startingPoint = First;
                }
                else
                {
                    startingPoint = Lasts;
                }
            }

            ParseLanes(startingPoint, componentParser, direction);
        }

        public void ParseLanes(List<GraphNode<T>> nodes, Action<GraphNode<T>> parser, Direction direction)
        {
            // Early exit
            if (nodes?.Any() == false || parser == null)
            {
                return;
            }

            foreach (var node in nodes)
            {
                parser(node);

                if (direction == Direction.Forwards)
                    ParseLanes(node.Next, parser, direction);
                else
                    ParseLanes(node.Previous, parser, direction);
            }
        }
    }
}