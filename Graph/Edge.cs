using System;
using System.Runtime.Serialization;

namespace Graph
{
    [DataContract]
    public class Edge<T>: IComparable<Edge<T>> where T: IComparable<T>
    {
        [DataMember]
        public Vertex<T> Start { get; }
        [DataMember]
        public Vertex<T> Finish { get; }
        [DataMember]
        public string Name { get; }
        [DataMember]
        public int Weight { get; }

        public Edge(T start, T finish, string name, int weight)
        {
            Start = new Vertex<T> (start);
            Finish = new Vertex<T> (finish);
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
