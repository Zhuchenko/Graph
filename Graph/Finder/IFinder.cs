using Graph.Structures;
using System;
using System.Collections.Generic;

namespace Graph
{
    public interface IFinder<T>
        where T:IComparable<T>
    {
        IEnumerable<Path<T>> Find(Graph<T> graph, 
            T starting, T final, IOption<T> option);
    }
}
