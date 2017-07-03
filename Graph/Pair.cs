using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    class Pair<TKey, TValue>
    {
        public TKey Key { get; }
        public TValue Value { get; set; }

        public Pair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}
