using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using J = Newtonsoft.Json.JsonPropertyAttribute;
using R = Newtonsoft.Json.Required;
using N = Newtonsoft.Json.NullValueHandling;

namespace Nodebb_Redis_KeyConverter.Classes
{
    public partial class Topic
    {
        [J("tid")] [JsonConverter(typeof(ParseStringConverter))] public long Tid { get; set; }
        [J("uid")] [JsonConverter(typeof(ParseStringConverter))] public long Uid { get; set; }
        [J("cid")] [JsonConverter(typeof(ParseStringConverter))] public long Cid { get; set; }
        [J("mainPid")] [JsonConverter(typeof(ParseStringConverter))] public long MainPid { get; set; }
        [J("title")] public string Title { get; set; }
        [J("slug")] public string Slug { get; set; }
        [J("timestamp")] public string Timestamp { get; set; }
        [J("lastposttime")] public string Lastposttime { get; set; }
        [J("postcount")] [JsonConverter(typeof(ParseStringConverter))] public long Postcount { get; set; }
        [J("viewcount")] [JsonConverter(typeof(ParseStringConverter))] public long Viewcount { get; set; }
        [J("locked")] [JsonConverter(typeof(ParseStringConverter))] public long Locked { get; set; }
        [J("teaserPid")] [JsonConverter(typeof(ParseStringConverter))] public long TeaserPid { get; set; }
    }
    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
