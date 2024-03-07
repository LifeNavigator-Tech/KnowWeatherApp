using System.Text.Json.Serialization;

namespace KnowWeatherApp.API.Models.OpenWeather;

public class WeatherReportDto
{
    public double Lat { get; set; }
    public double Lon { get; set; }
    public string? TimeZone { get; set; }

    [JsonPropertyName("timezone_offset")]
    public int TimeZoneOffset { get; set; }
    public CurrentWeatherReportDto Current { get; set; }
    public List<HourlyWeatherReportDto> Hourly { get; set; }
    public List<DailyWeatherReportDto> Daily { get; set; }
    public List<WeatherAlertDto> Alerts { get; set; }
}
