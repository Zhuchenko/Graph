using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Graph;

namespace WebAPI
{
    public class Input
    {
        public Graph<string> Graph { get; set; }
        public string Starting { get; set; }
        public string Final { get; set; }
        //public Option[] Options { get; set; }
    }
}