using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    public class Options<T>
    {
        public T[] Include { get; set; }
        public T[] Exclude { get; set; }

        public Options(IEnumerable<T> include, IEnumerable<T> minus)
        {
            Include = new T[include.Count()];
            Array.Copy(include.ToArray(), Include, include.Count());
            Exclude = new T[minus.Count()];
            Array.Copy(minus.ToArray(), Exclude, minus.Count());
        }
    }
}
