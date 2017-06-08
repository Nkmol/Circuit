using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Datatypes
{
    // Edge is a relation between 2 nodes
    public class Edge<T>
        where T : class 
    {
        public T From;
        public T To;

        public Edge(T from, T to = null)
        {
            From = from;
            To = to;
        }
    }
}
