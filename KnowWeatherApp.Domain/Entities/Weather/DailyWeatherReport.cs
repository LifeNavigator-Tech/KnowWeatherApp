using KnowWeatherApp.Domain.Helpers;
using System.Text.Json.Serialization;

namespace KnowWeatherApp.Domain.Entities.Weather;

public class DailyWeatherReport
{
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime DateTime { get; set; }
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime Sunrise { get; set; }
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime Sunset { get; set; }
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime MoonRise { get; set; }
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime MoonSet { get; set; }
    public int Pressure { get; set; }
    public double DewPoint { get; set; }
    public double WindSpeed { get; set; }
    public DailyTempDto Temp { get; set; }
    public DailyTempDto FeelsLike { get; set; }
}
