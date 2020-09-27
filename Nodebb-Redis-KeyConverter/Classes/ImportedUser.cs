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
    public partial class ImportedUser
    {
        [J("banned", NullValueHandling = N.Ignore)] [JsonConverter(typeof(PurpleParseStringConverter))] public long? Banned { get; set; }
        [J("_picture", NullValueHandling = N.Ignore)] public string Picture { get; set; }
        [J("_alternativeUsername", NullValueHandling = N.Ignore)] public string AlternativeUsername { get; set; }
        [J("_username", NullValueHandling = N.Ignore)] public string Username { get; set; }
        [J("_title", NullValueHandling = N.Ignore)] public string Title { get; set; }
        [J("status", NullValueHandling = N.Ignore)] public string Status { get; set; }
        [J("birthday", NullValueHandling = N.Ignore)] public string ImportedUserBirthday { get; set; }
        [J("email:confirmed", NullValueHandling = N.Ignore)] [JsonConverter(typeof(PurpleParseStringConverter))] public long? EmailConfirmed { get; set; }
        [J("lastposttime", NullValueHandling = N.Ignore)] [JsonConverter(typeof(PurpleParseStringConverter))] public long? Lastposttime { get; set; }
        [J("joindate", NullValueHandling = N.Ignore)] public string ImportedUserJoindate { get; set; }
        [J("_website", NullValueHandling = N.Ignore)] public string Website { get; set; }
        [J("website", NullValueHandling = N.Ignore)] public string ImportedUserWebsite { get; set; }
        [J("__imported_original_data__", NullValueHandling = N.Ignore)] public string ImportedOriginalData { get; set; }
        [J("_signature", NullValueHandling = N.Ignore)] public string Signature { get; set; }
        [J("location", NullValueHandling = N.Ignore)] public string Location { get; set; }
        [J("keptPicture", NullValueHandling = N.Ignore)] [JsonConverter(typeof(FluffyParseStringConverter))] public bool? KeptPicture { get; set; }
        [J("profileviews", NullValueHandling = N.Ignore)] [JsonConverter(typeof(PurpleParseStringConverter))] public long? Profileviews { get; set; }
        [J("_uid", NullValueHandling = N.Ignore)] [JsonConverter(typeof(PurpleParseStringConverter))] public long? Uid { get; set; }
        [J("signature", NullValueHandling = N.Ignore)] public string ImportedUserSignature { get; set; }
        [J("fullname", NullValueHandling = N.Ignore)] public string Fullname { get; set; }
        [J("_email", NullValueHandling = N.Ignore)] public string Email { get; set; }
        [J("_reputation", NullValueHandling = N.Ignore)] [JsonConverter(typeof(PurpleParseStringConverter))] public long? Reputation { get; set; }
        [J("userslug", NullValueHandling = N.Ignore)] public string Userslug { get; set; }
        [J("_joindate", NullValueHandling = N.Ignore)] public string Joindate { get; set; }
        [J("showemail", NullValueHandling = N.Ignore)] [JsonConverter(typeof(PurpleParseStringConverter))] public long? Showemail { get; set; }
        [J("_birthday", NullValueHandling = N.Ignore)] public string Birthday { get; set; }
        [J("imported", NullValueHandling = N.Ignore)] [JsonConverter(typeof(FluffyParseStringConverter))] public bool? Imported { get; set; }
        [J("reputation", NullValueHandling = N.Ignore)] [JsonConverter(typeof(PurpleParseStringConverter))] public long? ImportedUserReputation { get; set; }
        [J("uid", NullValueHandling = N.Ignore)] [JsonConverter(typeof(PurpleParseStringConverter))] public long? ImportedUserUid { get; set; }
        [J("_registrationEmail", NullValueHandling = N.Ignore)] public string RegistrationEmail { get; set; }
    }
    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class PurpleParseStringConverter : JsonConverter
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

        public static readonly PurpleParseStringConverter Singleton = new PurpleParseStringConverter();
    }

    internal class FluffyParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(bool) || t == typeof(bool?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            bool b;
            if (Boolean.TryParse(value, out b))
            {
                return b;
            }
            throw new Exception("Cannot unmarshal type bool");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (bool)untypedValue;
            var boolString = value ? "true" : "false";
            serializer.Serialize(writer, boolString);
            return;
        }

        public static readonly FluffyParseStringConverter Singleton = new FluffyParseStringConverter();
    }
}
