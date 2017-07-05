using System;

namespace Graph
{
    public class Edge<T>: IComparable<Edge<T>> where T: IComparable<T>
    {
        public Vertex<T> Starting { get; }
        public Vertex<T> Final { get; }
        public string Name { get; }
        public int Weight { get; }

        public Edge(Vertex<T> starting, Vertex<T> final, string name, int weight)
        {
            Starting = starting;
            Final = final;
            Name = name;
            Weight = weight;
        }

        public override string ToString()
        {
            return Name + "-" + Weight;
        }

        public int CompareTo(Edge<T> other)
        {
            return Weight - other.Weight;
        }
    }
}
