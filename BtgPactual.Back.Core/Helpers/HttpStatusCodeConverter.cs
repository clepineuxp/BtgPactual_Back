using Newtonsoft.Json;
using System.Net;

public class HttpStatusCodeConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        writer.WriteValue((int)(HttpStatusCode)value);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        return Enum.ToObject(typeof(HttpStatusCode), reader.Value);
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(HttpStatusCode);
    }
}