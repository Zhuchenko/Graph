using System;
using System.Linq;

namespace Graph
{
    public class Checker<T>
    {
        Options<T> _optForVertexes;
        Options<string> _optForEdges;
        int[] checkForIncludeVertexes;
        int[] checkForIncludeEdges;

        public Checker(Options<string> optForEdges, Options<T> optForVertexes)
        {
            _optForEdges = optForEdges;
            _optForVertexes = optForVertexes;
            checkForIncludeEdges = new int[_optForEdges.Include.Length];
            checkForIncludeVertexes = new int[_optForVertexes.Include.Length];
        }

        public bool CheckStartVertex(T item)
        {
            int index = Array.IndexOf(_optForVertexes.Include, item);
            if (index != -1) checkForIncludeVertexes[index]++;
            return !_optForVertexes.Exclude.Contains(item);
        }

        public bool CheckForExclude(string edge, T vertex)
        {
            return _optForEdges.Exclude.Contains(edge) || _optForVertexes.Exclude.Contains(vertex);
        }

        public void CheckForInclude(string edge, T vertex)
        {
            int index = Array.IndexOf(_optForEdges.Include, edge);
            if (index != -1) checkForIncludeEdges[index]++;
            index = Array.IndexOf(_optForVertexes.Include, vertex);
            if (index != -1) checkForIncludeVertexes[index]++;
        }

        public void UncheckForInclude(string edge, T vertex)
        {
            int index = Array.IndexOf(_optForEdges.Include, edge);
            if (index != -1) checkForIncludeEdges[index]--;
            index = Array.IndexOf(_optForVertexes.Include, vertex);
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
