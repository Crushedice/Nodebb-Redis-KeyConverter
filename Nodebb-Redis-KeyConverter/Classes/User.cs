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
    public partial class User
    {
        [J("username")] public string Username { get; set; }
        [J("userslug")] public string Userslug { get; set; }
        [J("email")] public string Email { get; set; }
        [J("joindate")] public string Joindate { get; set; }
        [J("lastonline")] public string Lastonline { get; set; }
        [J("status")] public string Status { get; set; }
        [J("gdpr_consent")] [JsonConverter(typeof(ParseStringConverter))] public long GdprConsent { get; set; }
        [J("uid")] [JsonConverter(typeof(ParseStringConverter))] public long Uid { get; set; }
        [J("password")] public string Password { get; set; }
        [J("rss_token")] public string RssToken { get; set; }
    }
}
