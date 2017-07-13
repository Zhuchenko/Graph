using Graph.Structures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    public class IncludeExclude<T> : IOption<T>
        where T : IComparable<T>
    {
        public IncludeExclude()
        {
            IncludeEdges = new string[0];
            IncludeVertexes = new T[0];
            ExcludeEdges = new string[0];
            ExcludeVertexes = new T[0];
        }

        public IncludeExclude(IEnumerable<string> incEdges, IEnumerable<T> incVertexes,
            IEnumerable<string> excEdges, IEnumerable<T> excVertexes)
        {
            IncludeEdges = incEdges?.ToArray() ?? new string[0];
            IncludeVertexes = incVertexes?.ToArray() ?? new T[0];
            ExcludeEdges = excEdges?.ToArray() ?? new string[0];
            ExcludeVertexes = excVertexes?.ToArray() ?? new T[0];
        }
        
        public string[] IncludeEdges { get; set; }
        public T[] IncludeVertexes { get; set; }

        public string[] ExcludeEdges { get; set; }
        public T[] ExcludeVertexes { get; set; }

        private bool CheckVertex(T vertex)
        {

            return ExcludeVertexes.Contains(vertex);
        }

        public bool CheckEdge(Edge<T> edge)
        {

            return !(ExcludeEdges.Contains(edge.Name)
                || CheckVertex(edge.Start.Key)
                || CheckVertex(edge.Finish.Key));
        }

        public bool CheckPath(Path<T> path)
        {
            var checker = new CheckerInclude<T>(IncludeEdges, IncludeVertexes);
            checker.CheckVertex(path.First().Start.Key);

            foreach (var edge in path)
                checker.CheckEdge(edge);

            return checker.CheckPath();
        }
    }
}
