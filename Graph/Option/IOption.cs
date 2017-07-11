using Graph.Structures;
using System;

namespace Graph
{
    public interface IOption<T> 
        where T: IComparable<T>
    {
        bool CheckEdge(Edge<T> edge);
        bool CheckPath(Path<T> path);
    }
}