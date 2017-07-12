namespace WebAPI
{
    public class Option
    {
        public TypeOfOption type { get; set; }

        public int sup { get; set; }

        public string[] includeEdges { get; set; }
        public string[] includeVertexes { get; set; }
        public string[] excludeEdges { get; set; }
        public string[] excludeVertexes { get; set; }
    }
}