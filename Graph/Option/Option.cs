using System;
using System.Collections.Generic;

namespace Graph
{
    public class Option<T> : IOption<T> where T : IComparable<T>
    {
        IOption<T>[] _options;

        public Option(IOption<T>[] options)
        {
            _options = options;
        }

        public bool CheckEdge(Edge<T> edge)
        {
            foreach(var item in _options)
                if (!item.CheckEdge(edge))
                    return false;
            return true;
        }

        public bool CheckPath(IEnumerable<Edge<T>> path)
        {
            foreach (var item in _options)
                if (!item.CheckPath(path))
                    return false;
            return true;
        }
    }
}
