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
<<<<<<< HEAD

        public Edge<T>[] Edges { get; set; }
=======
>>>>>>> 99eae3d6cda43347b3b8f20a08dae5a2ca923f73

        public Edge<T>[] Edges { get; set; }
    }
}
