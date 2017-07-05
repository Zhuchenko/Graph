using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    public class Graph<T> where T: IComparable<T>
    {
        public Edge<T>[] Edges { get; }

        public Graph(IEnumerable<Edge<T>> edges)
        {
            Edges = new Edge<T>[edges.Count()];
            Array.Copy(edges.ToArray(), Edges, edges.Count());
        }

        public IEnumerable<IEnumerable<Edge<T>>> GetPathesWithOptions
            (Vertex<T> Starting, Vertex<T> Final, Options<string> optForEdges, Options<T> optForVertexes)
        {
            var returnValue = new List<List<Edge<T>>>();
            var checker = new Checker<T>(optForEdges, optForVertexes);
            var firstEdges = from item in Edges where item.Starting.CompareTo(Starting) == 0 select item;
            foreach (Edge<T> e in firstEdges)
            {
                if (!checker.CheckStartVertex(e.Starting.Key)) break;
                checker.CheckForInclude(e.Name, e.Final.Key);
                var edges = new List<Pair<Edge<T>, List<Edge<T>>>>
                { new Pair<Edge<T>, List<Edge<T>>>(e, new List<Edge<T>>()) };
                while (edges.Count > 0)
                {
                    if (checker.CheckForExclude(edges.Last().Key.Name, edges.Last().Key.Final.Key))
                        RemoveEdge(edges, checker);
                    if (edges.Count == 0) break;

                    if (edges.Last().Key.Final.CompareTo(Final) == 0)
                    {
                        if (checker.CheckPath()) returnValue.Add((from t in edges select t.Key).ToList());
                        RemoveEdge(edges, checker);
                        if (edges.Count == 0) break;
                    }

                    Edge<T> nextEdge = FindNextEdge(edges);
                    if (nextEdge != null) AddEdge(edges, nextEdge, checker);
                    else RemoveEdge(edges, checker);
                }
            }
            return returnValue;
        }

        Edge<T> FindNextEdge(List<Pair<Edge<T>, List<Edge<T>>>> edges)
        {
            foreach (Edge<T> item in Edges)
                if (item.Starting.CompareTo(edges.Last().Key.Final) == 0
                    && !edges.Last().Value.Contains(item)
                    && edges.FindAll(x => x.Key == item).Count == 0)
                    return item;
            return null;
        }

        void RemoveEdge(List<Pair<Edge<T>, List<Edge<T>>>> edges, Checker<T> checker)
        {
            checker.UncheckForInclude(edges.Last().Key.Name, edges.Last().Key.Final.Key);
            edges.Remove(edges.Last());
        }

        void AddEdge(List<Pair<Edge<T>, List<Edge<T>>>> edges, Edge<T> edge, Checker<T> checker)
        {
            edges.Last().Value.Add(edge);
            edges.Add(new Pair<Edge<T>, List<Edge<T>>>(edge, new List<Edge<T>>()));
            checker.CheckForInclude(edges.Last().Key.Name, edges.Last().Key.Final.Key);
        }
    }
}
