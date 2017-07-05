namespace Graph
{
    public class Pair<TKey, TValue>
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
