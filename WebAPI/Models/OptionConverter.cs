using Graph;
using Graph.Option;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace WebAPI
{
    public class OptionConverter : JsonConverter
    {
        protected  IOption<string> Create(Type objectType, JObject jObject)
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

        public override bool CanConvert(Type objectType)
        {
            return typeof(IOption<string>).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader,
            Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);

            IOption<string> target = Create(objectType, jObject);

            serializer.Populate(jObject.CreateReader(), target);

            return target;
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}