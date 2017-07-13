using Graph;
using System.Web.Http;
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

            input.Options = new IOption<string>[]
            {
                new MaxLength<string>(2),
                new IncludeExclude<string>()
            };
            
            ((IncludeExclude<string>)input.Options[1]).ExcludeEdges = new string[] { "AC" };

            return Ok(input);
        }

        [HttpPost]
        [Route("best")]
        public IHttpActionResult FindBest([FromBody]Input obj)
        {
            var finder = new BestPathFinder<string>(new AllPathesFinder<string>());

            var bestPath = finder.Find(obj.Graph, obj.Starting, obj.Final, new OptionComposite<string>(obj.Options));
           
            return Ok(bestPath);
        }

        [HttpPost]
        [Route("all")]
        public IHttpActionResult FindAll([FromBody]Input obj)
        {
            var finder = new AllPathesFinder<string>();

            var allPathes = finder.Find(obj.Graph, obj.Starting, obj.Final, new OptionComposite<string>(obj.Options));

            return Ok(allPathes);
        }
    }
}
