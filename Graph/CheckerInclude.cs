using System;

namespace Graph
{
    public class CheckerInclude<T>
    {
        Options<T> _include;
        int[] checkForIncludeVertexes;
        int[] checkForIncludeEdges;

        public CheckerInclude(Options<T> include)
        {
            _include = include;
            checkForIncludeEdges = new int[_include.Edges.Length];
            checkForIncludeVertexes = new int[_include.Vertexes.Length];
        }

        public void CheckEdge(string edge, T item)
        {
            int index = Array.IndexOf(_include.Edges, edge);
            if (index != -1) checkForIncludeEdges[index]++;
            CheckVertex(item);
        }

        public void CheckVertex(T item)
        {
            int index = Array.IndexOf(_include.Vertexes, item);
            if (index != -1) checkForIncludeVertexes[index]++;
        }

        public void UnCheckEdge(string edge, T item)
        {
            int index = Array.IndexOf(_include.Edges, edge);
            if (index != -1) checkForIncludeEdges[index]--;
            UnCheckVertex(item);
        }

        public void UnCheckVertex(T item)
        {
            int index = Array.IndexOf(_include.Vertexes, item);
            if (index != -1) checkForIncludeVertexes[index]--;
        }

        public bool CheckPath()
        {
            foreach (int check in checkForIncludeEdges)
                if (check == 0) return false;
            foreach (int check in checkForIncludeVertexes)
                if (check == 0) return false;
            return true;
        }
    }
}
