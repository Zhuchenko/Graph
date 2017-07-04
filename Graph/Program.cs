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
            var S = new Vertex<string>(Console.ReadLine());
            Console.Write("F = ");
            var F = new Vertex<string>(Console.ReadLine());
            Console.Write("Input searching options: ");
            string option = Console.ReadLine();

            if (!Contains(graph, S) || !Contains(graph, F))
            {
                Console.WriteLine("Graph must contain S and F");
                Console.ReadKey();
                return;
            }

            var result = FindPathes(graph, S, F, option);
            if (result != null)
                Print(result);
            else Console.WriteLine("Incorrect format of searching options!");

            Console.ReadKey();
        }

        public static IEnumerable<Edge<string>> Read(string allpathes)
        {
            var returnValue = new List<Edge<string>>();
            using (StreamReader sr = new StreamReader(allpathes))
            {
                int n = int.Parse(sr.ReadLine());
                var del = new char[] { ' ', ',' };
                for (int i = 0; i < n; i++)
                {
                    string[] param = sr.ReadLine().Split(del, StringSplitOptions.RemoveEmptyEntries);
                    foreach (Edge<string> item in returnValue)
                    {
                        if (item.Name == param[2])
                            return null;
                    }
                    returnValue.Add(new Edge<string>(new Vertex<string>(param[0]), new Vertex<string>(param[1]), param[2]));
                }
            }
            return returnValue;
        }

        public static bool Contains(IEnumerable<Edge<string>> graph, Vertex<string> ver)
        {
            foreach (var item in graph)
                if (item.Starting.CompareTo(ver) == 0 || item.Final.CompareTo(ver) == 0)
                    return true;
            
            return false;
        }

        public static List<List<Edge<string>>> FindPathes(IEnumerable<Edge<string>> graph, Vertex<string> S, Vertex<string> F, string op)
        {
            var returnValue = new List<List<Edge<string>>>();

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

            var firstEdges = from item in graph where item.Starting.CompareTo(S) == 0 select item;
            foreach (Edge<string> e in firstEdges)
            {
                if (minus.Contains(e.Starting.Name)) break;
                var checkForPlus = new int[plus.Count];
                int ind = plus.IndexOf(e.Starting.Name);
                if (ind != -1) checkForPlus[ind]++;

                var edges = new List<Pair<Edge<string>, List<Edge<string>>>>
                { new Pair<Edge<string>, List<Edge<string>>>(e, new List<Edge<string>>()) };
                while (edges.Count > 0)
                {
                    if (minus.Contains(edges.Last().Key.Name) || minus.Contains(edges.Last().Key.Final.Name))
                    {
                        ind = plus.IndexOf(edges.Last().Key.Name);
                        if (ind != -1) checkForPlus[ind]--;
                        ind = plus.IndexOf(edges.Last().Key.Final.Name);
                        if (ind != -1) checkForPlus[ind]--;
                        edges.Remove(edges.Last());
                        continue;
                    }

                    ind = plus.IndexOf(edges.Last().Key.Name);
                    if (ind != -1) checkForPlus[ind]++;
                    ind = plus.IndexOf(edges.Last().Key.Final.Name);
                    if (ind != -1) checkForPlus[ind]++;

                    if (edges.Last().Key.Final.CompareTo(F) == 0)
                    {
                        bool flag = true;
                        foreach (int check in checkForPlus)
                            if (check == 0)
                            {
                                flag = false;
                                break;
                            }

                        if (flag) returnValue.Add((from t in edges select t.Key).ToList());

                        ind = plus.IndexOf(edges.Last().Key.Name);
                        if (ind != -1) checkForPlus[ind]--;
                        ind = plus.IndexOf(edges.Last().Key.Final.Name);
                        if (ind != -1) checkForPlus[ind]--;

                        edges.Remove(edges.Last());
                        if (edges.Count == 0) break;
                    }

                    Edge<string> nextEdge = null;
                    foreach (Edge<string> item in graph)
                        if (item.Starting.CompareTo(edges.Last().Key.Final) == 0
                            && !edges.Last().Value.Contains(item)
                            && edges.FindAll(x => x.Key == item).Count == 0)
                            nextEdge = item;
                    if (nextEdge != null)
                    {
                        edges.Last().Value.Add(nextEdge);
                        edges.Add(new Pair<Edge<string>, List<Edge<string>>>(nextEdge, new List<Edge<string>>()));
                    }
                    else
                    {
                        ind = plus.IndexOf(edges.Last().Key.Name);
                        if (ind != -1) checkForPlus[ind]--;
                        ind = plus.IndexOf(edges.Last().Key.Final.Name);
                        if (ind != -1) checkForPlus[ind]--;
                        edges.Remove(edges.Last());
                    }
                }
            }

            return returnValue;
        }
        
        public static void Print(List<List<Edge<string>>> result)
        {
            for (int i = 0; i < result.Count; i++)
            {
                Console.Write("Path #" + (i + 1) + ": ");
                foreach (Edge<string> item in result[i])
                    Console.Write("  " + item);
                Console.WriteLine();
            }

            if (result.Count == 0)
                Console.WriteLine("No path");
        }
    }
}
