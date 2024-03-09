using System.Text.Json.Serialization;
using KnowWeatherApp.Contracts.Helpers;

namespace KnowWeatherApp.Contracts.OpenWeather;

public class DailyWeatherReportDto
{
    [JsonPropertyName("dt")]
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime DateTime { get; set; }
    [JsonPropertyName("sunrise")]
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime Sunrise { get; set; }
    [JsonPropertyName("sunset")]
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime Sunset { get; set; }
    [JsonPropertyName("moonrise")]
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime MoonRise { get; set; }
    [JsonPropertyName("moonset")]
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime MoonSet { get; set; }
    [JsonPropertyName("pressure")]
    public int Pressure { get; set; }
    [JsonPropertyName("dew_point")]
    public double DewPoint { get; set; }
    [JsonPropertyName("wind_speed")]
    public double WindSpeed { get; set; }
    [JsonPropertyName("temp")]
    public DailyTempDto Temp { get; set; }
    [JsonPropertyName("feels_like")]
    public DailyTempDto FeelsLike { get; set; }
}
