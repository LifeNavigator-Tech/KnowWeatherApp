﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnowWeatherApp.Domain.Entities.Weather;

public class WeatherReport
{
    [Key]
    [ForeignKey("City")]
    public string CityId { get; set; }
    public virtual City City { get; set; }
    public double Lat { get; set; }
    public double Lon { get; set; }
    public string? TimeZone { get; set; }
    public int TimeZoneOffset { get; set; }
    public CurrentWeatherReport Current { get; set; } = new CurrentWeatherReport();
    public List<HourlyWeatherReport> HourlyReports { get; set; } = new List<HourlyWeatherReport>();
    public List<DailyWeatherReport> DailyReports { get; set; } = new List<DailyWeatherReport>();
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}
