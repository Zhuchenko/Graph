using System;
using System.Diagnostics;

namespace Graph
{
    [DebuggerDisplay("{Name}-{Weight}")]
    public class Edge<T>: IComparable<Edge<T>> 
        where T: IComparable<T>
    {
        public Vertex<T> Start { get; }
        public Vertex<T> Finish { get; }

        public string Name { get; }

        public int Weight { get; }

        public Edge(T start, T finish, string name, int weight)
        {
            Start = new Vertex<T> (start);
            Finish = new Vertex<T> (finish);
            Name = name;
            Weight = weight;
        }

        public int CompareTo(Edge<T> other)
        {
            return Weight - other.Weight;
        }
    }
}
