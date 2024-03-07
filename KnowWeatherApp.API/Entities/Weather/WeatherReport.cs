namespace KnowWeatherApp.API.Entities.Weather;

public class WeatherReport
{
    public City City { get; set; }
    public double Lat { get; set; }
    public double Lon { get; set; }
    public string? TimeZone { get; set; }
    public int TimeZoneOffset { get; set; }
    public CurrentWeatherReport Current { get; set; }
    public List<HourlyWeatherReport> Hourly { get; set; }
    public List<DailyWeatherReport> Daily { get; set; }
}
