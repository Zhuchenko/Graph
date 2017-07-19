using Graph.Structures;
using System;

namespace Graph
{
    public class OptionComposite<T> : IOption<T> 
        where T : IComparable<T>
    {
        private readonly IOption<T>[] _options;

        public OptionComposite(IOption<T>[] options)
        {
            _options = (options != null) ? options : new IOption<T>[0];
        }

        public bool CheckEdge(Edge<T> edge)
        {
            foreach (var item in _options)
            {
                if (!item.CheckEdge(edge))
                    return false;
            }

            return true;
        }

        public bool CheckPath(Path<T> path)
        {
            foreach (var item in _options)
            {
                if (!item.CheckPath(path))
                    return false;
            }

            return true;
        }
    }
}
