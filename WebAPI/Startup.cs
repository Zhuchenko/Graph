using System.Net.Http.Formatting;
using Owin;
using System.Web.Http;
using Microsoft.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using WebAPI;
using System.Collections.Generic;

[assembly: OwinStartup(typeof(Startup))]

namespace WebAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var httpConfiguration = new HttpConfiguration();

            SetFormatters(httpConfiguration.Formatters);

            httpConfiguration.MapHttpAttributeRoutes();
            appBuilder.UseWebApi(httpConfiguration);
        }

        private void SetFormatters(MediaTypeFormatterCollection formatters)
        {
            formatters.Clear();

            formatters.Add(new JsonMediaTypeFormatter
            {
                SerializerSettings = Json()
            });
        }

        public static JsonSerializerSettings Json(TypeNameHandling typeNameHandling = TypeNameHandling.None)
        {
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = typeNameHandling,
                DefaultValueHandling = DefaultValueHandling.Include,
                NullValueHandling = NullValueHandling.Ignore,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Converters = new List<JsonConverter>
                {
                    new OptionConverter()
                }
            };

            settings.Converters.Add(new StringEnumConverter { CamelCaseText = false });
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            return settings;
        }
    }
}
