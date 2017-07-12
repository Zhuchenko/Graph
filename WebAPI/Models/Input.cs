using Graph;
using Graph.Option;

namespace WebAPI
{
    public class Input
    {
        public Graph<string> Graph { get; set; }

        public string Starting { get; set; }

        public string Final { get; set; }

        public Option[] Options { get; set; }

        public OptionComposite<string> GetOptionComposite()
        {
            IOption<string>[] opts = new IOption<string>[Options.Length];

            for (int i = 0; i < Options.Length; i++)
            {
                var item = Options[i];

                if (item.type == TypeOfOption.MaxLength)
                {
                    opts[i] = new MaxLength<string>(item.sup);
                }

                if (item.type == TypeOfOption.IncludeExclude)
                {
                    CheckIncludeExclude(i);
                    opts[i] = new IncludeExclude<string>(item.includeEdges, item.includeVertexes,
                        item.excludeEdges, item.excludeVertexes);
                }
            }

            return new OptionComposite<string>(opts);
        }

        private void CheckIncludeExclude(int i)
        {
            if (Options[i].includeEdges == null)
                Options[i].includeEdges = new string[0];

            if (Options[i].includeVertexes == null)
                Options[i].includeVertexes = new string[0];

            if (Options[i].excludeEdges == null)
                Options[i].excludeEdges = new string[0];

            if (Options[i].excludeVertexes == null)
                Options[i].excludeVertexes = new string[0];
        }
    }
}