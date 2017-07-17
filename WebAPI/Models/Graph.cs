using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Graph
    {
        public string[] Vertexes { get; set; }
        public Edge[] Edges { get; set; }
    }
}