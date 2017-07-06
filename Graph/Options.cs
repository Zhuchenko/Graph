using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    public class Options<T>
    {
        public string[] Edges { get; set; }
        public T[] Vertexes { get; set; }

        public Options(IEnumerable<string> e, IEnumerable<T> v)
        {
            Edges = new string[e.Count()];
            Array.Copy(e.ToArray(), Edges, e.Count());
            Vertexes = new T[v.Count()];
            Array.Copy(v.ToArray(), Vertexes, v.Count());
        }
    }
}
