using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KnowWeatherApp.Contracts.Helpers
{
    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var unixTimeInSeconds = reader.GetDouble();
            return epoch.AddSeconds(unixTimeInSeconds);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("G"));
        }
    }
}

