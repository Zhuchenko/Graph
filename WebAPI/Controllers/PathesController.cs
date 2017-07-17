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
            var graph = new Models.Graph()
            {
                Vertexes = new string[] { "A", "B", "C", "D", },
                Edges = new Models.Edge[]
                {
                    new Models.Edge()
                    {
                        Source = "A",
                        Target = "B"
                    },
                    new Models.Edge()
                    {
                        Source = "A",
                        Target = "C"
                    },
                    new Models.Edge()
                    {
                        Source = "A",
                        Target = "D"
                    },
                    new Models.Edge()
                    {
                        Source = "B",
                        Target = "D"
                    },
                    new Models.Edge()
                    {
                        Source = "D",
                        Target = "C"
                    }
                }
            };

            return Ok(graph);
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
