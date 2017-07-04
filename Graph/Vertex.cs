using System;

namespace Graph
{
    class Vertex<T>: IComparable<Vertex<T>> where T: IComparable<T>
    {
        public string Name { get; set; }

        public Vertex(string name)
        {
            Name = name;
        }

        public int CompareTo(Vertex<T> other)
        {
            return Name.CompareTo(other.Name);
        }
    }
}
