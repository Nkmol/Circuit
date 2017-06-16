namespace Datatypes.DirectedGraph
{
    using System;
    using System.Collections.Generic;

    // Edge is a relation between 2 nodes
    public class Edge<T> : IEquatable<Edge<T>>
        where T : class
    {
        public T From { get; }
        public T To { get; }
        public List<T> Path { get; set; } = new List<T>();

        public Edge(T from, T to = null)
        {
            From = from;
            To = to;
        }

        public bool Equals(Edge<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<T>.Default.Equals(From, other.From) && EqualityComparer<T>.Default.Equals(To, other.To);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Edge<T>) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (EqualityComparer<T>.Default.GetHashCode(From) * 397) ^ EqualityComparer<T>.Default.GetHashCode(To);
            }
        }
    }
}