using KnowWeatherApp.Contracts.Helpers;
using KnowWeatherApp.Contracts.OpenWeather;
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
    public int Humidity { get; set; }
    public double DewPoint { get; set; }
    public double WindSpeed { get; set; }
    public double WindGust { get; set; }
    public DailyTempDto Temp { get; set; }
    public DailyTempDto FeelsLike { get; set; }
    public List<GeneralWeather> Weather { get; set; } = new List<GeneralWeather>();
    public int Clouds { get; set; }
    public double Precipitation { get; set; }
    public double Rain { get; set; }
    public double Uvi { get; set; }
}
