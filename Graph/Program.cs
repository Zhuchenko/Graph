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
            Console.Write("Input path to file: ");
            string pathToFile = Console.ReadLine();
            var graph = Read(pathToFile);

            if (graph == null)
            {
                Console.WriteLine("Every name of edge must be one and only!");
                Console.ReadKey();
                return;
            }

            Console.Write("S = ");
            string S = Console.ReadLine();
            Console.Write("F = ");
            string F = Console.ReadLine();
            Console.Write("Input searching options: ");
            string options = Console.ReadLine();

            if (!Contains(graph, S) || !Contains(graph, F))
            {
                Console.WriteLine("Graph must contain S and F");
                Console.ReadKey();
                return;
            }

            var allpathes = FindAllPathes(graph, S, F);

            var resultAND = FindPathesWithOptionsAND(allpathes, options).ToList();
            var resultOR = FindPathesWithOptionsOR(allpathes, options).ToList();

            if (resultAND == null || resultOR == null)
            {
                Console.WriteLine("Incorrect format of searching options!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nAllPathes");
            Print(allpathes);

            Console.WriteLine("\nPathesWithOptionsAND");
            Print(resultAND);

            Console.WriteLine("\nPathesWithOptionsOR");
            Print(resultOR);

            Console.ReadKey();
        }

        public static IEnumerable<Edge> Read(string allpathes)
        {
            var returnValue = new List<Edge>();
            using (StreamReader sr = new StreamReader(allpathes))
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

        public static List<List<Edge>> FindAllPathes(IEnumerable<Edge> graph, string S, string F)
        {
            var returnValue = new List<List<Edge>>();
            var firstEdges = from item in graph where item.Starting == S select item;
            foreach (Edge e in firstEdges)
            {
                var edges = new List<Pair<Edge, List<Edge>>> { new Pair<Edge, List<Edge>>(e, new List<Edge>()) };
                while (edges.Count > 0)
                {
                    if (edges.Last().Key.Final == F)
                    {
                        returnValue.Add((from t in edges select t.Key).ToList());
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
                        edges.Remove(edges.Last());
                }
            }
            return returnValue;
        }

        public static IEnumerable<List<Edge>> FindPathesWithOptionsAND(IEnumerable<List<Edge>> allPathes, string op)
        {
            var unsuitable = new List<List<Edge>>();
            var options = op.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            var plus = new List<string>();
            var minus = new List<string>();
            foreach(var item in options)
            {
                if (item[0] == '-')
                    minus.Add(item.TrimStart('-'));
                else if (item[0] == '+')
                    plus.Add(item.TrimStart('+'));
                else return null;
            }

            foreach(var path in allPathes)
            {
                var checkForPlus = new bool[plus.Count];
                if (minus.Contains(path[0].Starting))
                {
                    unsuitable.Add(path);
                    continue;
                }
                int ind = plus.IndexOf(path[0].Starting);
                if (ind != -1)
                {
                    checkForPlus[ind] = true;
                }
                foreach (Edge item in path)
                {
                    if (minus.Contains(item.Final) || minus.Contains(item.Name))
                    {
                        unsuitable.Add(path);
                        break;
                    }
                    ind = plus.IndexOf(item.Final);
                    if (ind != -1)
                    {
                        checkForPlus[ind] = true;
                    }
                    ind = plus.IndexOf(item.Name);
                    if (ind != -1)
                    {
                        checkForPlus[ind] = true;
                    }
                }
                foreach(var check in checkForPlus)
                    if (!check)
                    {
                        unsuitable.Add(path);
                        break;
                    }
            }
            var returnValue = from path in allPathes where !unsuitable.Contains(path) select path;
            return returnValue;
        }

        public static IEnumerable<List<Edge>> FindPathesWithOptionsOR(IEnumerable<List<Edge>> allPathes, string op)
        {
            var returnValue = new List<List<Edge>>();
            var options = op.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            var plus = new List<string>();
            var minus = new List<string>();
            foreach (var item in options)
            {
                if (item[0] == '-')
                    minus.Add(item.TrimStart('-'));
                else if (item[0] == '+')
                    plus.Add(item.TrimStart('+'));
                else return null;
            }

            foreach (var path in allPathes)
            {
                var checkForMinus = new bool[minus.Count];
                if (plus.Contains(path[0].Starting))
                {
                    returnValue.Add(path);
                    continue;
                }
                int ind = minus.IndexOf(path[0].Starting);
                if (ind != -1)
                {
                    checkForMinus[ind] = true;
                }
                foreach (Edge item in path)
                {
                    if (plus.Contains(item.Final) || plus.Contains(item.Name))
                    {
                        returnValue.Add(path);
                        break;
                    }
                    ind = minus.IndexOf(item.Final);
                    if (ind != -1)
                    {
                        checkForMinus[ind] = true;
                    }
                    ind = minus.IndexOf(item.Name);
                    if (ind != -1)
                    {
                        checkForMinus[ind] = true;
                    }
                }
                foreach (var check in checkForMinus)
                    if (!check)
                    {
                        returnValue.Add(path);
                        break;
                    }
            }
            return returnValue;
        }

        public static void Print(List<List<Edge>> result)
        {
            for (int i = 0; i < result.Count; i++)
            {
                Console.Write("Path #" + (i + 1) + ": ");
                foreach (Edge item in result[i])
                    Console.Write("  " + item);
                Console.WriteLine();
            }

            if (result.Count == 0)
                Console.WriteLine("No path");
        }
    }
}
