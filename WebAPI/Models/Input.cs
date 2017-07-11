using Graph;
using System;
using System.Runtime.Serialization;

namespace WebAPI.Models
{
    [DataContract]
    public class Input<T> where T : IComparable<T>
    {
        [DataMember]
        public Graph<T> Graph { get; }
        [DataMember]
        public T Starting { get; }
        [DataMember]
        public T Final { get; }
        [DataMember]
        public Option<T> Option { get; }

        public Input(Graph<T> graph, T starting, T final, Option<T> option)
        {
            Graph = graph;
            Starting = starting;
            Final = final;
            Option = option;
        }
    }
}