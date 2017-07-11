using System;
using System.Runtime.Serialization;

namespace Graph
{

    [DataContract]
    public class Vertex<T>: IComparable<Vertex<T>> where T: IComparable<T>
    {
        [DataMember]
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
