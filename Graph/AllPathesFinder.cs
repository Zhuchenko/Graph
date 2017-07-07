using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    public class AllPathesFinder<T> where T: IComparable<T>
    {
        public IEnumerable<IEnumerable<Edge<T>>> GirthOfGraph(Graph<T> graph, T starting, T final, IOption<T> option)
        {
            var firstEdges = from item in graph.Edges where item.Start.Key.CompareTo(starting) == 0 select item;
            foreach (Edge<T> e in firstEdges)
            {
                if (!option.CheckEdge(e)) continue;
                var edges = new List<Pair<Edge<T>, List<Edge<T>>>>
                { new Pair<Edge<T>, List<Edge<T>>>(e, new List<Edge<T>>()) };
                while (edges.Count > 0)
                {
                    var path = (from item in edges select item.Key);
                    if (edges.Last().Key.Finish.Key.CompareTo(final) == 0 && option.CheckPath(path))
                    {
                        yield return path;
                        edges.Remove(edges.Last());
                        if (edges.Count == 0) break;
                    }

                    Edge<T> nextEdge = FindNextEdge(graph, edges, option);
                    if (nextEdge != null)
                        AddEdgeInGirth(edges, nextEdge);
                    else
                        edges.Remove(edges.Last());
                }
            }
            yield break;
        }

        Edge<T> FindNextEdge(Graph<T> graph, List<Pair<Edge<T>, List<Edge<T>>>> edges, IOption<T> option)
        {
            foreach (Edge<T> item in graph.Edges)
                if (item.Start.CompareTo(edges.Last().Key.Finish) == 0
                    && !edges.Last().Value.Contains(item)
                    && edges.FindAll(x => x.Key == item).Count == 0
                    && option.CheckEdge(item))
                    return item;
            return null;
        }

        void AddEdgeInGirth(List<Pair<Edge<T>, List<Edge<T>>>> edges, Edge<T> edge)
        {
            edges.Last().Value.Add(edge);
            edges.Add(new Pair<Edge<T>, List<Edge<T>>>(edge, new List<Edge<T>>()));
        }
    }
}
