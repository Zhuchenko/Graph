using Graph;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class PathesController : ApiController
    {
        public IHttpActionResult Post([FromBody]Input obj)
        {
            var finder = new BestPathFinder<string>();
            //var bestPath = finder.Find(obj.Graph, obj.Starting, obj.Final, obj.Option);
            //string answer = "";
            //foreach (var edge in bestPath)
            //    answer += edge.ToString() + "\n";
            //return answer;
            return Ok("OK");
        }
    }
}
