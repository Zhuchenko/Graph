using System.Web.Http;

namespace WebAPI
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "AllPathes",
                routeTemplate: "pathes",
                defaults: new { controller = "Pathes", action = "Post" }
                );
        }
    }
}