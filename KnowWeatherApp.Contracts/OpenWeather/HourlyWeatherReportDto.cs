using System.Text.Json.Serialization;
using KnowWeatherApp.Contracts.Helpers;

namespace KnowWeatherApp.Contracts.OpenWeather;

public class HourlyWeatherReportDto
{
    [JsonPropertyName("dt")]
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime DateTime { get; set; }
    [JsonPropertyName("temp")]
    public double Temp { get; set; }
    [JsonPropertyName("feels_like")]
    public double FeelsLike { get; set; }
    [JsonPropertyName("humidity")]
    public int Humidity { get; set; }
    [JsonPropertyName("dew_point")]
    public double DewPoint { get; set; }
    [JsonPropertyName("uvi")]
    public double Uvi { get; set; }
    [JsonPropertyName("clouds")]
    public int Clouds { get; set; }
    [JsonPropertyName("visibility")]
    public int Visibility { get; set; }
    [JsonPropertyName("wind_speed")]
    public double Windspeed { get; set; }
    [JsonPropertyName("wind_deg")]
    public int WindDeg { get; set; }
    [JsonPropertyName("wind_gust")]
    public double WindGust { get; set; }
    [JsonPropertyName("pop")]
    public double Precipitation { get; set; }
}
