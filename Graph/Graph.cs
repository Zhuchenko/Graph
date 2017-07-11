using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Graph
{
    [DataContract]
    public class Graph<T> where T: IComparable<T>
    {
        [DataMember]
        public Edge<T>[] Edges { get; }

        public Graph(IEnumerable<Edge<T>> edges)
        {
            Edges = new Edge<T>[edges.Count()];
            Array.Copy(edges.ToArray(), Edges, edges.Count());
        }
    }
}
