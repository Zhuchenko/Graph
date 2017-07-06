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
        
        public IEnumerable<IEnumerable<Edge<T>>> GirthOfGraph(Vertex<T> starting, Vertex<T> final, Options<T> exclude)
        {
            var firstEdges = from item in Edges where item.Starting.CompareTo(starting) == 0 select item;
            foreach (Edge<T> e in firstEdges)
            {
                if (exclude.Vertexes.Contains(e.Starting.Key)) yield break;
                if (exclude.Edges.Contains(e.Name) || exclude.Vertexes.Contains(e.Final.Key)) continue;
                var edges = new List<Pair<Edge<T>, List<Edge<T>>>>
                { new Pair<Edge<T>, List<Edge<T>>>(e, new List<Edge<T>>()) };
                var section = new List<Edge<T>> { e };
                while (edges.Count > 0)
                {
                    if (edges.Last().Key.Final.CompareTo(final) == 0)
                    {
                        yield return section;
                        section = new List<Edge<T>>();
                        RemoveEdgeInGirth(edges, section);
                        if (edges.Count == 0) break;
                    }

                    Edge<T> nextEdge = FindNextEdge(edges, exclude);
                    if (nextEdge != null)
                        AddEdgeInGirth(edges, section, nextEdge);
                    else
                        RemoveEdgeInGirth(edges, section);
                }
            }
            yield break;
        }

        Edge<T> FindNextEdge(List<Pair<Edge<T>, List<Edge<T>>>> edges, Options<T> exclude)
        {
            foreach (Edge<T> item in Edges)
                if (item.Starting.CompareTo(edges.Last().Key.Final) == 0
                    && !edges.Last().Value.Contains(item)
                    && edges.FindAll(x => x.Key == item).Count == 0
                    && !exclude.Edges.Contains(item.Name)
                    && !exclude.Vertexes.Contains(item.Final.Key))
                    return item;
            return null;
        }

        void AddEdgeInGirth(List<Pair<Edge<T>, List<Edge<T>>>> edges, List<Edge<T>> section, Edge<T> edge)
        {
            edges.Last().Value.Add(edge);
            edges.Add(new Pair<Edge<T>, List<Edge<T>>>(edge, new List<Edge<T>>()));
            section.Add(edge);
        }

        void RemoveEdgeInGirth(List<Pair<Edge<T>, List<Edge<T>>>> edges, List<Edge<T>> section)
        {
            edges.Remove(edges.Last());
            if (section.Count != 0) section.Remove(section.Last());
        }

        public IEnumerable<IEnumerable<Edge<T>>> FindAllPathesWithOptions
            (Vertex<T> Starting, Vertex<T> Final, Options<T> include, Options<T> exclude)
        {
            var allPathes = new List<List<Edge<T>>>();
            var currentPath = new List<Pair<Edge<T>, List<Edge<T>>>>();
            var checker = new CheckerInclude<T>(include);
            foreach (var section in GirthOfGraph(Starting, Final, exclude))
            {
                while (currentPath.Count != 0 &&
                    (currentPath.Last().Key.Final.CompareTo(section.First().Starting) != 0
                    || currentPath.Last().Value.Contains(section.First())))
                {
                    RemoveEdge(currentPath, checker);
                }
                if (currentPath.Count == 0) checker = new CheckerInclude<T>(include);
                foreach (var edge in section)
                    AddEdge(currentPath, edge, checker);
                if (checker.CheckPath()) allPathes.Add((from item in currentPath select item.Key).ToList());
            }

            return allPathes;
        }

        public IEnumerable<Edge<T>> FindBestPathWithOptions
            (Vertex<T> Starting, Vertex<T> Final, Options<T> include, Options<T> exclude)
        {
            var bestPath = new List<Edge<T>>();
            var currentPath = new List<Pair<Edge<T>, List<Edge<T>>>>();
            var checker = new CheckerInclude<T>(include);
            int minWeight = -1;
            int currentWeight = 0;
            foreach (var section in GirthOfGraph(Starting, Final, exclude))
            {
                bool completed = true;
                while (currentPath.Count != 0 &&
                    (currentPath.Last().Key.Final.CompareTo(section.First().Starting) != 0
                    || currentPath.Last().Value.Contains(section.First())))
                {
                    currentWeight -= currentPath.Last().Key.Weight;
                    RemoveEdge(currentPath, checker);
                }
                if (currentPath.Count == 0) checker = new CheckerInclude<T>(include);
                foreach (var edge in section)
                {
                    if (minWeight == -1 || minWeight >= currentWeight + edge.Weight)
                    {
                        AddEdge(currentPath, edge, checker);
                        currentWeight += edge.Weight;
                    }
                    else
                    {
                        completed = false;
                        break;
                    }
                }
                if (completed && checker.CheckPath() && (minWeight == -1 || minWeight > currentWeight || bestPath.Count > currentPath.Count))
                {
                    bestPath = (from item in currentPath select item.Key).ToList();
                    minWeight = currentWeight;
                }
            }
            return bestPath;
        }

        void AddEdge(List<Pair<Edge<T>, List<Edge<T>>>> currentPath, Edge<T> edge, CheckerInclude<T> checker)
        {
            if(currentPath.Count == 0)
                checker.CheckVertex(edge.Starting.Key);
            else currentPath.Last().Value.Add(edge);
            checker.CheckEdge(edge.Name, edge.Final.Key);
            currentPath.Add(new Pair<Edge<T>, List<Edge<T>>>(edge, new List<Edge<T>>()));
        }

        void RemoveEdge(List<Pair<Edge<T>, List<Edge<T>>>> currentPath, CheckerInclude<T> checker)
        {
            checker.UnCheckEdge(currentPath.Last().Key.Name, currentPath.Last().Key.Final.Key);
            currentPath.Remove(currentPath.Last());
        }
    }
}
