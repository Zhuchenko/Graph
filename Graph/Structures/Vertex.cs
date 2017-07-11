using System;

namespace Graph
{
    public class Vertex<T>: IComparable<Vertex<T>> 
        where T: IComparable<T>
    {
        public T Key { get; set; }

        public Vertex(T key)
        {
            Key = key;
        }

        public int CompareTo(Vertex<T> other)
        {
            return Key.CompareTo(other.Key);
        }
    }
}
