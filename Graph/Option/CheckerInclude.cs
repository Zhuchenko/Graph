using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Graph
{
    [DataContract]
    public class CheckerInclude<T> where T: IComparable<T>
    {
        [DataMember]
        string[] includeEdges;
        [DataMember]
        T[] includeVertexes;

        [DataMember]
        bool[] checkEdges;
        [DataMember]
        bool[] checkVertexes;

        public CheckerInclude(IEnumerable<string> incEdges, IEnumerable<T> incVertexes)
        {
            includeEdges = incEdges.ToArray();
            includeVertexes = incVertexes.ToArray();
            checkEdges = new bool[includeEdges.Length];
            checkVertexes = new bool[includeVertexes.Length];
        }

        public void CheckEdge(Edge<T> edge)
        {
            int index = Array.IndexOf(includeEdges, edge.Name);
            if (index != -1) checkEdges[index] = true;
            CheckVertex(edge.Finish.Key);
        }

        public void CheckVertex(T item)
        {
            int index = Array.IndexOf(includeVertexes, item);
            if (index != -1) checkVertexes[index] = true;
        }

        public bool CheckPath()
        {
            foreach (bool check in checkVertexes)
                if (!check) return false;
            foreach (bool check in checkEdges)
                if (!check) return false;
            return true;
        }
    }
}
