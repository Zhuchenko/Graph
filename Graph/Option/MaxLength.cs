using Graph.Structures;
using System;
using System.Linq;
using Newtonsoft.Json;

namespace Graph.Option
{
    public class MaxLength<T> : IOption<T>
        where T : IComparable<T>
    {
        public MaxLength() { }

        public MaxLength(int supremum)
        {
            Supremum = supremum;
        }

        [JsonProperty("supremum")]
        public int Supremum { get; set; }

        public bool CheckEdge(Edge<T> edge)
        {
            return true;
        }

        public bool CheckPath(Path<T> path)
        {
            return path.Count() <= Supremum;
        }
    }
}
