using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    public class IncludeExclude<T>: IOption<T> where T: IComparable<T>
    {
        string[] includeEdges;
        T[] includeVertexes;
        string[] excludeEdges;
        T[] excludeVertexes;

        CheckerInclude<T> checker;

        public IncludeExclude(IEnumerable<string> incEdges, IEnumerable<T> incVertexes, 
            IEnumerable<string> excEdges, IEnumerable<T> excVertexes)
        {
            includeEdges = incEdges.ToArray();
            includeVertexes = incVertexes.ToArray();
            excludeEdges = excEdges.ToArray();
            excludeVertexes = excVertexes.ToArray();
        }

        public bool CheckEdge(Edge<T> edge)
        {
            return !(excludeEdges.Contains(edge.Name) 
                || excludeVertexes.Contains(edge.Start.Key) 
                || excludeVertexes.Contains(edge.Finish.Key));
        }

        public bool CheckPath(IEnumerable<Edge<T>> path)
        {
            checker = new CheckerInclude<T>(includeEdges, includeVertexes);
            checker.CheckVertex(path.First().Start.Key);
            foreach (var edge in path)
                checker.CheckEdge(edge);
            return checker.CheckPath();
        }
    }
}
