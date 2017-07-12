using Graph;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Collections.Generic;
using Graph.Option;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/pathes")]
    public class PathesController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            var input = new Input();

            input.Starting = "A";

            input.Final = "C";

            input.Graph = new Graph<string>(new List<Edge<string>>
            {
                new Edge<string>("A", "B", "AB", 1),
                new Edge<string>("A", "C", "AC", 3),
                new Edge<string>("A", "D", "AD", 1),
                new Edge<string>("B", "A", "BA", 3),
                new Edge<string>("B", "C", "BC", 1),
                new Edge<string>("D", "C", "DC", 1)
            });

            input.Options = new Option[]
            {
                new Option(),
                new Option()
            };

            input.Options[0].type = TypeOfOption.MaxLength;
            input.Options[0].sup = 2;

            input.Options[1].type = TypeOfOption.IncludeExclude;
            input.Options[1].excludeEdges = new string[] { "AC" };

            return Ok(input);
        }

        [HttpPost]
        [Route("best")]
        public IHttpActionResult FindBest([FromBody]Input obj)
        {
            var finder = new BestPathFinder<string>();

            var optionComposite = obj.GetOptionComposite();

            var bestPath = finder.Find(obj.Graph, obj.Starting, obj.Final, optionComposite);
           
            return Ok(bestPath);
        }

        [HttpPost]
        [Route("all")]
        public IHttpActionResult FindAll([FromBody]Input obj)
        {
            var finder = new AllPathesFinder<string>();

            var optionComposite = obj.GetOptionComposite();

            var bestPath = finder.Find(obj.Graph, obj.Starting, obj.Final, optionComposite);

            return Ok(bestPath);
        }
    }
}
