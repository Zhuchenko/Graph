using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    class Graph<T> where T: IComparable<T>
    {
        public Edge<T>[] Edges { get; }

        public Graph(IEnumerable<Edge<T>> edges)
        {
            Edges = new Edge<T>[edges.Count()];
            Array.Copy(edges.ToArray(), Edges, edges.Count());
        }

        public IEnumerable<IEnumerable<Edge<T>>> GetPathesSFWithOptions
            (Vertex<T> S, Vertex<T> F, Dictionary<string, char> optionsForEdges, Dictionary<T, char> optionsForVertexes)
        {
            var returnValue = new List<List<Edge<T>>>();
            
            var plusForEdges = new List<string>();
            var minusForEdges = new List<string>();
            foreach (var item in optionsForEdges.Keys)
            {
                if (optionsForEdges[item] == '+')
                    plusForEdges.Add(item);
                if (optionsForEdges[item] == '-')
                    minusForEdges.Add(item);
            }

            var plusForVertexes = new List<T>();
            var minusForVertexes = new List<T>();
            foreach (var item in optionsForVertexes.Keys)
            {
                if (optionsForVertexes[item] == '+')
                    plusForVertexes.Add(item);
                if (optionsForVertexes[item] == '-')
                    minusForVertexes.Add(item);
            }

            var firstEdges = from item in Edges where item.Starting.CompareTo(S) == 0 select item;
            foreach (Edge<T> e in firstEdges)
            {
                if (minusForVertexes.Contains(e.Starting.Key)) break;
                var checkForPlusForEdges = new int[plusForEdges.Count];
                var checkForPlusForVertexes = new int[plusForVertexes.Count];
                int ind = plusForVertexes.IndexOf(e.Starting.Key);
                if (ind != -1) checkForPlusForVertexes[ind]++;

                var edges = new List<Pair<Edge<T>, List<Edge<T>>>>
                { new Pair<Edge<T>, List<Edge<T>>>(e, new List<Edge<T>>()) };
                while (edges.Count > 0)
                {
                    if (minusForEdges.Contains(edges.Last().Key.Name) || minusForVertexes.Contains(edges.Last().Key.Final.Key))
                    {
                        ind = plusForEdges.IndexOf(edges.Last().Key.Name);
                        if (ind != -1) checkForPlusForEdges[ind]--;
                        ind = plusForVertexes.IndexOf(edges.Last().Key.Final.Key);
                        if (ind != -1) checkForPlusForVertexes[ind]--;
                        edges.Remove(edges.Last());
                        continue;
                    }

                    ind = plusForEdges.IndexOf(edges.Last().Key.Name);
                    if (ind != -1) checkForPlusForEdges[ind]++;
                    ind = plusForVertexes.IndexOf(edges.Last().Key.Final.Key);
                    if (ind != -1) checkForPlusForVertexes[ind]++;

                    if (edges.Last().Key.Final.CompareTo(F) == 0)
                    {
                        bool flag = true;
                        foreach (int check in checkForPlusForEdges)
                            if (check == 0)
                            {
                                flag = false;
                                break;
                            }
                        if (flag)
                            foreach (int check in checkForPlusForVertexes)
                                if (check == 0)
                                {
                                    flag = false;
                                    break;
                                }

                        if (flag) returnValue.Add((from t in edges select t.Key).ToList());

                        ind = plusForEdges.IndexOf(edges.Last().Key.Name);
                        if (ind != -1) checkForPlusForEdges[ind]--;
                        ind = plusForVertexes.IndexOf(edges.Last().Key.Final.Key);
                        if (ind != -1) checkForPlusForVertexes[ind]--;

                        edges.Remove(edges.Last());
                        if (edges.Count == 0) break;
                    }

                    Edge<T> nextEdge = null;
                    foreach (Edge<T> item in Edges)
                        if (item.Starting.CompareTo(edges.Last().Key.Final) == 0
                            && !edges.Last().Value.Contains(item)
                            && edges.FindAll(x => x.Key == item).Count == 0)
                            nextEdge = item;
                    if (nextEdge != null)
                    {
                        edges.Last().Value.Add(nextEdge);
                        edges.Add(new Pair<Edge<T>, List<Edge<T>>>(nextEdge, new List<Edge<T>>()));
                    }
                    else
                    {
                        ind = plusForEdges.IndexOf(edges.Last().Key.Name);
                        if (ind != -1) checkForPlusForEdges[ind]--;
                        ind = plusForVertexes.IndexOf(edges.Last().Key.Final.Key);
                        if (ind != -1) checkForPlusForVertexes[ind]--;
                        edges.Remove(edges.Last());
                    }
                }
            }

            return returnValue;
        }
    }
}
