using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Graph.Option;

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
            var starting = Console.ReadLine();
            Console.Write("Final = ");
            var final = Console.ReadLine();

            Console.Write("Input searching options for edges: ");
            string optionsForEdges = Console.ReadLine();
            Console.Write("Input searching options for vertexes: ");
            string optionsForVertexes = Console.ReadLine();

            var option1 = DoOptions(optionsForEdges, optionsForVertexes);
            var option = new Option<string>(new IOption<string>[] { option1, new MaxLength<string>(5) });
            var bestPathFinder = new BestPathFinder<string>();
            var allPathesFinder = new AllPathesFinder<string>();

            foreach (var path in allPathesFinder.GirthOfGraph(graph, starting, final, option))
                Print(path);

            Print(bestPathFinder.Find(graph, starting, final, option));

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
                    returnValue.Add(new Edge<string>(param[0], param[1], param[2], int.Parse(param[3])));
                }
            }
            return returnValue;
        }

        public static IncludeExclude<string> DoOptions(string forEdges, string forVertexes)
        {
            var edge = DoOptionsForOne(forEdges);
            var vertex = DoOptionsForOne(forVertexes);
            return new IncludeExclude<string>(edge.Item1, vertex.Item1, edge.Item2, vertex.Item2);
        }

        public static Tuple<List<string>, List<string>> DoOptionsForOne(string input)
        {
            var options = input.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            var include = new List<string>();
            var exclude = new List<string>();
            foreach (var item in options)
            {
                if (item[0] == '+')
                    include.Add(item.Substring(1));
                if (item[0] == '-')
                    exclude.Add(item.Substring(1));
            }
            return Tuple.Create(include, exclude);
        }

        public static void Print(IEnumerable<Edge<string>> result)
        {
            var output = result.ToList();
            foreach (Edge<string> item in output)
                Console.Write("  " + item);

            if (result.Count() == 0)
                Console.WriteLine("No path");

            Console.WriteLine();
        }
    }
}
