using Graph;
using Graph.Option;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI
{
    public class OptionConverter : JsonCreationConverter<IOption<string>>
    {
        protected override IOption<string> Create(Type objectType, JObject jObject)
        {
            if (FieldExists("supremum", jObject))
            {
                return new MaxLength<string>();
            }
            else
            {
                return new IncludeExclude<string>();
            }
        }

        private bool FieldExists(string fieldName, JObject jObject)
        {
            return jObject[fieldName] != null;
        }
        
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JToken t = JToken.FromObject(value);

            if (t.Type != JTokenType.Object)
            {
                t.WriteTo(writer);
            }
            else
            {
                JObject obj = (JObject)t;
                IList<string> propertyNames = obj.Properties().Select(p => p.Name).ToList();

                obj.AddFirst(new JProperty("Options", new JArray(propertyNames)));

                obj.WriteTo(writer);
            }
        }
    }
}