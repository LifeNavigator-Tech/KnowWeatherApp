using KnowWeatherApp.Contracts.Helpers;
using System.Text.Json.Serialization;

namespace KnowWeatherApp.Domain.Entities.Weather;
public class CurrentWeatherReport
{
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime DateTime { get; set; }
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime Sunrise { get; set; }
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime Sunset { get; set; }
    public double Temp { get; set; }
    public int Pressure { get; set; }
    public double FeelsLike { get; set; }
    public int Humidity { get; set; }
    public double DewPoint { get; set; }
    public double Uvi { get; set; }
    public int Clouds { get; set; }
    public int Visibility { get; set; }
    public double Windspeed { get; set; }
    public int WindDeg { get; set; }
    public double WindGust { get; set; }
}
