using Graph.Structures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    public class BestPathFinder<T> : IFinder<T>
        where T : IComparable<T>
    {
        private readonly IFinder<T> _finder;

        public BestPathFinder(IFinder<T> finder)
        {
            _finder = finder;
        }

        public IEnumerable<Path<T>> Find(Graph<T> graph, T starting, T final, IOption<T> option)
        {
            var bestPath = new Path<T>();
            int minWeight = -1;
            foreach (var path in _finder.Find(graph, starting, final, option))
            {
                bool completed = true;
                int currentWeight = 0;

                if (minWeight == -1)
                {
                    bestPath = path;
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

                if (completed && (minWeight > currentWeight || bestPath.Count > path.Count))
                {
                    bestPath = path;
                    minWeight = currentWeight;
                }
            }

            var returnValue = new Path<T>[]
            {
                bestPath
            };

            return returnValue;
        }
    }
}
