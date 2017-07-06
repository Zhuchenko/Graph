using System;
using System.Collections.Generic;
using Graph;

namespace UnitTests
{
    public static class Builder
    {
        public static Graph<string> Build(List<Tuple<string, string, int>> param)
        {
            var edges = new List<Edge<string>>();
            foreach(var item in param)
            {
                edges.Add(new Edge<string>(new Vertex<string>(item.Item1), 
                    new Vertex<string>(item.Item2), 
                    item.Item1 + item.Item2, item.Item3));
            }
            return new Graph<string>(edges);
        }
    }
}
