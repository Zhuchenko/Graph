using System;
using System.Collections.Generic;
using Graph;

namespace UnitTests
{
    public static class Builder
    {
        public static Edge<string> BuildEdge(string start, string finish, int weight)
        {
            return new Edge<string>(start, finish, start + finish, weight);
        }

        public static List<Edge<string>> BuildListOfEdge(List<Tuple<string, string, int>> param)
        {
            var edges = new List<Edge<string>>();
            foreach(var item in param)
                edges.Add(BuildEdge(item.Item1, item.Item2, item.Item3));
            return edges;
        }

        public static Graph<string> BuildGraph(List<Tuple<string, string, int>> param)
        {
            return new Graph<string>(BuildListOfEdge(param));
        }
    }
}
