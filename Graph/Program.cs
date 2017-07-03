using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Graph
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input path");
            string path = Console.ReadLine();
            var graph = Read(path);
            Console.Write("S = ");
            string S = Console.ReadLine();
            Console.Write("F = ");
            string F = Console.ReadLine();

            if (graph == null)
            {
                Console.WriteLine("Every name of edge must be one and only!");
                Console.ReadKey();
                return;
            }
            if (!Contains(graph, S) || !Contains(graph, F))
            {
                Console.WriteLine("Graph must contain S and F");
                Console.ReadKey();
                return;
            }

            var result = new List<List<Edge>>();

            var firstEdges = from item in graph where item.Starting == S select item;
            foreach (Edge e in firstEdges)
            {
                var edges = new List<Pair<Edge, List<Edge>>> { new Pair<Edge, List<Edge>>(e, new List<Edge>()) };
                while (edges.Count > 0)
                {
                    if (edges.Last().Key.Final == F)
                    {
                        result.Add((from t in edges select t.Key).ToList());
                        edges.Remove(edges.Last());
                        continue;
                    }
                    Edge nextEdge = null;
                    foreach (Edge item in graph)
                        if (item.Starting == edges.Last().Key.Final
                            && !edges.Last().Value.Contains(item)
                            && edges.FindAll(x => x.Key == item).Count == 0)
                            nextEdge = item;
                    if (nextEdge != null)
                    {
                        edges.Last().Value.Add(nextEdge);
                        edges.Add(new Pair<Edge, List<Edge>>(nextEdge, new List<Edge>()));
                    }
                    else
                    {
                        //if (edges.Count > 1)
                            edges.Remove(edges.Last());
                        //else break;
                    }
                }
            }

            for (int i = 0; i < result.Count; i++)
            {
                Console.WriteLine("Path #" + (i + 1));
                foreach (var item in result[i])
                    Console.Write("  " + item);
                Console.WriteLine();
            }

            if (result.Count == 0)
                Console.WriteLine("No path");

            Console.ReadKey();
        }

        public static IEnumerable<Edge> Read(string path)
        {
            var returnValue = new List<Edge>();
            using (StreamReader sr = new StreamReader(path))
            {
                int n = int.Parse(sr.ReadLine());
                var del = new char[] { ' ', ',' };
                for (int i = 0; i < n; i++)
                {
                    string[] param = sr.ReadLine().Split(del, StringSplitOptions.RemoveEmptyEntries);
                    int index = 0;
                    foreach (Edge item in returnValue)
                    {
                        if (item.Name == param[2])
                            return null;
                        if (item.Starting == param[0] && item.Final == param[1])
                            index++;
                    }
                    returnValue.Add(new Edge(param[0], param[1], index, param[2]));
                }
            }
            return returnValue;
        }

        public static bool Contains(IEnumerable<Edge> graph, string vertex)
        {
            foreach(var item in graph)
                if (item.Starting == vertex || item.Final == vertex)
                    return true;
            
            return false;
        }
    }
}
