using System;
using System.Diagnostics;

namespace Graph
{
    [DebuggerDisplay("{Name}-{Weight}")]
    public class Edge<T> : IComparable<Edge<T>>
        where T : IComparable<T>
    {
        public Edge() { }

        public Edge(T start, T finish, string name, int weight)
        {
            Start = new Vertex<T>(start);
            Finish = new Vertex<T>(finish);
            Name = name;
            Weight = weight;
        }

        public Vertex<T> Start { get; set; }
        public Vertex<T> Finish { get; set; }

        public string Name { get; set; }

        public int Weight { get; set; }


        public int CompareTo(Edge<T> other)
        {
            return Weight - other.Weight;
        }
    }
}
