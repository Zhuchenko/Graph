using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    public class IncludeExclude<T>: IOption<T> 
        where T: IComparable<T>
    {
        private readonly string[] includeEdges;
        private readonly T[] includeVertexes;

        private readonly string[] excludeEdges;
        private readonly T[] excludeVertexes;
        
        private CheckerInclude<T> checker;

        public IncludeExclude(IEnumerable<string> incEdges, IEnumerable<T> incVertexes, 
            IEnumerable<string> excEdges, IEnumerable<T> excVertexes)
        {
            includeEdges = incEdges.ToArray();
            includeVertexes = incVertexes.ToArray();
            excludeEdges = excEdges.ToArray();
            excludeVertexes = excVertexes.ToArray();
        }

        private bool CheckVertex(T vertex)
        {

            return excludeVertexes.Contains(vertex);
        }

        public bool CheckEdge(Edge<T> edge)
        {

            return !(excludeEdges.Contains(edge.Name) 
                || CheckVertex(edge.Start.Key) 
                || CheckVertex(edge.Finish.Key));
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
