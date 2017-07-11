using Graph.Structures;
using System;
using System.Linq;

namespace Graph.Option
{
    public class MaxLength<T> : IOption<T> 
        where T : IComparable<T>
    {
        private readonly int _supremum;

        public MaxLength(int supremum)
        {
            _supremum = supremum;
        }

        public bool CheckEdge(Edge<T> edge)
        {
            return true;
        }

        public bool CheckPath(Path<T> path)
        {
            return path.Count() <= _supremum;
        }
    }
}
