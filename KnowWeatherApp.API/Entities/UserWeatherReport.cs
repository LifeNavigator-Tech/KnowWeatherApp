using KnowWeatherApp.API.Entities.Weather;

namespace KnowWeatherApp.API.Entities;

public class UserWeatherReport
{
    public string Id { get; set; }
    public List<City> Cities { get; set; }
    public List<WeatherReport> WeatherReports { get; set; }
    public DateTime? Updated { get; set; }
}