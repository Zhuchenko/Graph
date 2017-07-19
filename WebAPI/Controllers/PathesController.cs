using Graph;
using System.Web.Http;

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
                Vertexes = new string[] { "A", "B", "C", "D", "E", "F", "G"},
                Edges = new Models.Edge[]
                {
                    new Models.Edge() {
                        Source = "A", Target = "B", Id = "AB1", Weight = 1 },
                    new Models.Edge() {
                        Source = "A", Target = "B", Id = "AB2", Weight = 1 },
                    new Models.Edge() {
                        Source = "B", Target = "A", Id = "BA", Weight = 1 },
                    new Models.Edge() {
                        Source = "B", Target = "C", Id = "BC", Weight = 1 },
                    new Models.Edge() {
                        Source = "B", Target = "F", Id = "BF", Weight = 1 },
                    new Models.Edge() {
                        Source = "C", Target = "D", Id = "CD", Weight = 1 },
                    new Models.Edge() {
                        Source = "C", Target = "E", Id = "CE", Weight = 1 },
                    new Models.Edge() {
                        Source = "C", Target = "G", Id = "CG", Weight = 1 },
                    new Models.Edge() {
                        Source = "D", Target = "D", Id = "DD", Weight = 1 },
                    new Models.Edge() {
                        Source = "D", Target = "E", Id = "DE", Weight = 1 },
                    new Models.Edge() {
                        Source = "D", Target = "F", Id = "DF", Weight = 1 },
                    new Models.Edge() {
                        Source = "D", Target = "G", Id = "DG", Weight = 1 },
                    new Models.Edge() {
                        Source = "E", Target = "F", Id = "EF", Weight = 1 },
                    new Models.Edge() {
                        Source = "F", Target = "E", Id = "FE", Weight = 1 },
                    new Models.Edge() {
                        Source = "F", Target = "F", Id = "FF", Weight = 1 },
                    new Models.Edge() {
                        Source = "G", Target = "F", Id = "GF", Weight = 1 }
                }
            };

            return Ok(graph);
        }

        [HttpPost]
        [Route("best")]
        public IHttpActionResult FindBest([FromBody]Input obj)
        {
            var finder = new BestPathFinder<string>(new AllPathesFinder<string>());

            var graph = new Graph<string>() {
                Edges = obj.Graph };

            var bestPath = finder.Find(graph, obj.Starting, obj.Final, new OptionComposite<string>(obj.Options));
           
            return Ok(bestPath);
        }

        [HttpPost]
        [Route("all")]
        public IHttpActionResult FindAll([FromBody]Input obj)
        {
            var finder = new AllPathesFinder<string>();

            var graph = new Graph<string>()
            {
                Edges = obj.Graph
            };

            var allPathes = finder.Find(graph, obj.Starting, obj.Final, new OptionComposite<string>(obj.Options));

            return Ok(allPathes);
        }
    }
}
