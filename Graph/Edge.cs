namespace Graph
{
    class Edge
    {
        public string Starting { get; }
        public string Final { get; }
        public int Index { get; }
        public string Name { get; }

        public Edge(string starting, string final, int index, string name)
        {
            Starting = starting;
            Final = final;
            Index = index;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
