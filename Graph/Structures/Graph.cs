using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    public class Graph<T> 
        where T: IComparable<T>
    {
        public Graph() { }

        public Graph(IEnumerable<Edge<T>> edges)
        {
            Edges = new Edge<T>[edges.Count()];
            Array.Copy(edges.ToArray(), Edges, edges.Count());
        }

        public Edge<T>[] Edges { get; set; }
        
        public IEnumerable<Edge<T>> FindAllBeginingIn(T vertex)
        {
            return from edge in Edges
                   where edge.Start.Key.CompareTo(vertex) == 0
                   select edge;
        }
    }
}
