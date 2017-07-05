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

            Console.Write("Starting = ");
            var Starting = new Vertex<string>(Console.ReadLine());
            Console.Write("Final = ");
            var Final = new Vertex<string>(Console.ReadLine());

            if (!Contains(graph, Starting) || !Contains(graph, Final))
            {
                Console.WriteLine("Graph must contain Starting and Final");
                Console.ReadKey();
                return;
            }

            Console.Write("Input searching options for edges: ");
            string optionsForEdges = Console.ReadLine();
            Console.Write("Input searching options for vertexes: ");
            string optionsForVertexes = Console.ReadLine();

            var optForEdges = DoOptions(optionsForEdges);
            var optForVertexes = DoOptions(optionsForVertexes);

            var result = graph.GetPathesWithOptions(Starting, Final, optForEdges, optForVertexes);
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
                    returnValue.Add(new Edge<string>(new Vertex<string>(param[0]), new Vertex<string>(param[1]), param[2], int.Parse(param[3])));
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

        public static Options<string> DoOptions(string input)
        {
            var options = input.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            var plus = new List<string>();
            var minus = new List<string>();
            foreach (var item in options)
            {
                if (item[0] == '-')
                    minus.Add(item.Substring(1));
                if (item[0] == '+')
                    plus.Add(item.Substring(1));
            }
            return new Options<string>(plus, minus); ;
        }

        public static void Print(IEnumerable<Edge<string>> result)
        {
            var output = result.ToList();
            foreach (Edge<string> item in output)
                Console.Write("  " + item);

            if (result.Count() == 0)
                Console.WriteLine("No path");
        }
    }
}
