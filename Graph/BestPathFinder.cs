using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    public class BestPathFinder<T> where T: IComparable<T>
    {
        public IEnumerable<Edge<T>> Find(Graph<T> graph, T starting, T final, IOption<T> option)
        {
            var bestPath = new List<Edge<T>>();
            int minWeight = -1;
            var finder = new AllPathesFinder<T>();
            foreach (var path in finder.GirthOfGraph(graph, starting, final, option))
            {
                bool completed = true;
                int currentWeight = 0;
                if (minWeight == -1)
                {
                    bestPath = path.ToList();
                    minWeight = (from edge in bestPath select edge.Weight).Sum();
                    continue;
                }
                foreach (var edge in path)
                {
                    if (currentWeight + edge.Weight > minWeight)
                    {
                        completed = false;
                        break;
                    }
                    currentWeight += edge.Weight;
                }
                if (completed && (minWeight > currentWeight || bestPath.Count > path.Count()))
                {
                    bestPath = path.ToList();
                    minWeight = currentWeight;
                }
            }
            return bestPath;
        }
    }
}
