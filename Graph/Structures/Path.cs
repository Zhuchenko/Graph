using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Graph.Structures
{
    public class Path<T>: ICloneable, IEnumerable<Edge<T>>
        where T: IComparable<T>
    {
        public Path()
        {
            Edges = new List<Edge<T>>();
            Count = 0;
        }

        public Path(Edge<T> edge)
        {
            Edges = new List<Edge<T>>
            {
                edge
            };
            Count = 1;
        }

        public Path(IEnumerable<Edge<T>> edges)
        {
            Edges = edges.ToList();
            Count = Edges.Count;
        }

        public List<Edge<T>> Edges { get; }

        public int Count { get; private set; }
        
        public void Add(Edge<T> edge)
        {
            Edges.Add(edge);
            Count++;
        }

        public bool Contains(Edge<T> edge)
        {
            return Edges.Contains(edge);
        }

        public Edge<T> Last()
        {
            return Edges.Last();
        }

        public object Clone()
        {
            return new Path<T>(Edges.GetRange(0, Count));
        }

        public bool EndsWith(T vertex)
        {
            return Last().Finish.Key.CompareTo(vertex) == 0;
        }

        public bool ContinuesWith(Edge<T> edge)
        {
            return Last().Finish.Key.CompareTo(edge.Start.Key) == 0;
        }

        public IEnumerator<Edge<T>> GetEnumerator()
        {
            foreach (var edge in Edges)
            {
                yield return edge;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Edges.GetEnumerator();
        }
    }
}
