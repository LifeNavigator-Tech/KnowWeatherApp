using System.Text.Json;
using System.Text.Json.Serialization;

namespace KnowWeatherApp.API.Helpers;

public class CustomDateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var unixTimeInSeconds = reader.GetDouble();
        return DateTime.UnixEpoch.AddSeconds(unixTimeInSeconds);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("G"));
    }
}