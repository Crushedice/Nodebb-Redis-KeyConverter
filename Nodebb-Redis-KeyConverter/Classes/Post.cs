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
    public partial class Post
    {
        [J("ip")] public string Ip { get; set; }
        [J("toPid")] [JsonConverter(typeof(ParseStringConverter))] public long ToPid { get; set; }
        [J("pid")] [JsonConverter(typeof(ParseStringConverter))] public long Pid { get; set; }
        [J("timestamp")] public string Timestamp { get; set; }
        [J("content")] public string Content { get; set; }
        [J("tid")] [JsonConverter(typeof(ParseStringConverter))] public long Tid { get; set; }
        [J("uid")] [JsonConverter(typeof(ParseStringConverter))] public long Uid { get; set; }
    }
}
