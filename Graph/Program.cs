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
            var graph = new Graph<string>(Read(pathToFile));

            Console.Write("S = ");
            var S = new Vertex<string>(Console.ReadLine());
            Console.Write("F = ");
            var F = new Vertex<string>(Console.ReadLine());

            if (!Contains(graph, S) || !Contains(graph, F))
            {
                Console.WriteLine("Graph must contain S and F");
                Console.ReadKey();
                return;
            }

            Console.Write("Input searching options for edges: ");
            string optionsForEdges = Console.ReadLine();
            Console.Write("Input searching options for vertexes: ");
            string optionsForVertexes = Console.ReadLine();

            var dictForEdges = DoDict(optionsForEdges);
            var dictForVertexes = DoDict(optionsForVertexes);

            var result = graph.GetPathesSFWithOptions( S, F, dictForEdges, dictForVertexes);
            Print(result);

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
                    returnValue.Add(new Edge<string>(new Vertex<string>(param[0]), new Vertex<string>(param[1]), param[2]));
                }
            }
            return returnValue;
        }

        public static bool Contains(Graph<string> graph, Vertex<string> ver)
        {
            foreach (var item in graph.Edges)
                if (item.Starting.CompareTo(ver) == 0 || item.Final.CompareTo(ver) == 0)
                    return true;
            
            return false;
        }

        public static Dictionary<string, char> DoDict(string op)
        {
            var options = op.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            var dict = new Dictionary<string, char>();
            foreach (var item in options)
            {
                if (item[0] == '-')
                    dict.Add(item.TrimStart('-'), '-');
                if (item[0] == '+')
                    dict.Add(item.TrimStart('+'), '+');
            }
            return dict;
        }

        public static void Print(IEnumerable<IEnumerable<Edge<string>>> result)
        {
            for (int i = 0; i < result.Count(); i++)
            {
                Console.Write("Path #" + (i + 1) + ": ");
                var res_i = result.ToList()[i];
                foreach (Edge<string> item in res_i)
                    Console.Write("  " + item);
                Console.WriteLine();
            }

            if (result.Count() == 0)
                Console.WriteLine("No path");
        }
    }
}
