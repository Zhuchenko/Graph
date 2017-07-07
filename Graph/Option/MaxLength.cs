using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph.Option
{
    public class MaxLength<T> : IOption<T> where T : IComparable<T>
    {
        int _sup;

        public MaxLength(int sup)
        {
            _sup = sup;
        }

        public bool CheckEdge(Edge<T> edge)
        {
            return true;
        }

        public bool CheckPath(IEnumerable<Edge<T>> path)
        {
            return path.Count() <= _sup;
        }
    }
}
