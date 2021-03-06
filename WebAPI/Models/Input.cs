﻿using Graph;
using Graph.Option;

namespace WebAPI
{
    public class Input
    {
        public Edge<string>[] Graph { get; set; }

        public string Starting { get; set; }

        public string Final { get; set; }

        public IOption<string>[] Options { get; set; }
    }
}