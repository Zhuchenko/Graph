using System;

namespace Graph
{
    public class Vertex<T>: IComparable<Vertex<T>> 
        where T: IComparable<T>
    {
        public Vertex() { }

        public Vertex(T key)
        {
            Key = key;
        }

        public T Key { get; set; }

        public int CompareTo(Vertex<T> other)
        {
            return Key.CompareTo(other.Key);
        }
    }
}
