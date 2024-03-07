using KnowWeatherApp.API.Models.OpenWeather;

namespace KnowWeatherApp.API.Models;

public class UserWeatherReportDto
{
    public List<CityDto> Cities { get; set; }
    public List<WeatherReportDto> WeatherReports { get; set; }
    public DateTime? Updated { get; set; } = DateTime.UtcNow;
}
