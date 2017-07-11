using System;
using System.Collections.Generic;

namespace Graph
{
    public interface IOption<T> 
        where T: IComparable<T>
    {
        bool CheckEdge(Edge<T> edge);
        bool CheckPath(IEnumerable<Edge<T>> path);
    }
}