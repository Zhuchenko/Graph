using System;

namespace Graph
{
    public class Edge<T> where T: IComparable<T>
    {
        public Vertex<T> Starting { get; }
        public Vertex<T> Final { get; }
        public string Name { get; }

        public Edge(Vertex<T> starting, Vertex<T> final, string name)
        {
            Starting = starting;
            Final = final;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
