using System.ComponentModel.DataAnnotations.Schema;

namespace KnowWeatherApp.API.Entities.Weather;

public class WeatherReport
{
    public int WeatherReportId { get; set; }
    [ForeignKey("City")]
    public string CityId { get; set; }
    public virtual City City { get; set; }
    public double Lat { get; set; }
    public double Lon { get; set; }
    public string? TimeZone { get; set; }
    public int TimeZoneOffset { get; set; }
    public CurrentWeatherReport Current { get; set; }
    public List<HourlyWeatherReport> HourlyReports { get; set; }
    public List<DailyWeatherReport> DailyReports { get; set; }
}
