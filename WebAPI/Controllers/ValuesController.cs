using System.Collections.Generic;
using System.Web.Http;
using Graph;
using Graph.Option;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class ValuesController : ApiController
    {
        // POST api/values
        public string Post([FromBody]Input<string> value)
        {
            var finder = new BestPathFinder<string>();
            var bestPath = finder.Find(value.Graph, value.Starting, value.Final, value.Option);
            string answer = "";
            foreach (var edge in bestPath)
                answer += edge.ToString() + "\n";
            return answer;
        }
    }
}
