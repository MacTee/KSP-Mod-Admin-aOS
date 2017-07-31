using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KSPMODAdmin.Core.Utils.Ckan
{
    /// <summary>
    /// With thanks to https://stackoverflow.com/questions/18994685/how-to-handle-both-a-single-item-and-an-array-for-the-same-property-using-json-n
    /// and CKAN ;)
    /// </summary>
    /// <typeparam name="T">Type of the single/array value(s).</typeparam>    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public class JsonSingleOrArrayConverter<T> : JsonConverter
    {
        public override bool CanConvert(Type object_type)
        {
            // We *only* want to be triggered for types that have explicitly
            // set an attribute in their class saying they can be converted.
            // By returning false here, we declare we're not interested in participating
            // in any other conversions.
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            if (token.Type == JTokenType.Array)
            {
                return token.ToObject<List<T>>();
            }

            // If the object is null, we'll return null. Otherwise end up with a list of null.
            return token.ToObject<T>() == null ? null : new List<T> { token.ToObject<T>() };
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