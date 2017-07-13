using Graph.Structures;
using System;
using System.Collections.Generic;

namespace Graph
{
    public class AllPathesFinder<T> : IFinder<T>
        where T : IComparable<T>
    {
        public IEnumerable<Path<T>> Find(Graph<T> graph, T starting, T final, IOption<T> option)
        {
            var queue = new Queue<Path<T>>();
            var firstEdges = graph.FindAllBeginingIn(starting);

            foreach (var edge in firstEdges)
                queue.Enqueue(new Path<T>(edge));

            while (queue.Count > 0)
            {
                Path<T> currentPath = queue.Dequeue();

                var isCompleted = currentPath.EndsWith(final);
                var isChecked = option.CheckPath(currentPath);

                if (isCompleted && isChecked)
                {
                    yield return currentPath;
                    continue;
                }

                var nextEdges = FindAllNextEdges(graph, currentPath, option);

                foreach (var edge in nextEdges)
                {
                    var newPath = CreatePath(currentPath, edge);
                    queue.Enqueue(newPath);
                }
            }
        }
        
        private IEnumerable<Edge<T>> FindAllNextEdges(Graph<T> graph, Path<T> currentPath, IOption<T> option)
        {
            var nextEdges = new List<Edge<T>>();

            foreach (Edge<T> item in graph.Edges)
            {
                var isContinuation = currentPath.ContinuesWith(item);
                var isNotContains = !currentPath.Contains(item);
                var isChecked = option.CheckEdge(item);

                if (isContinuation && isNotContains && isChecked)
                {
                    nextEdges.Add(item);
                }
            }

            return nextEdges;
        }

        private Path<T> CreatePath(Path<T> currentPath, Edge<T> nextEdge)
        {
            var returnValue = (Path<T>)currentPath.Clone();
            returnValue.Add(nextEdge);

            return returnValue;
        }
    }
}
