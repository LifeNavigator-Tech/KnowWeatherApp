﻿using System.Text.Json.Serialization;
using KnowWeatherApp.Contracts.Helpers;
using KnowWeatherApp.Contracts.OpenWeather;
namespace KnowWeatherApp.Domain.Entities.Weather;

public class HourlyWeatherReport
{
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime DateTime { get; set; }
    public double Temp { get; set; }
    public double FeelsLike { get; set; }
    public int Pressure { get; set; }
    public int Humidity { get; set; }
    public double DewPoint { get; set; }
    public double Uvi { get; set; }
    public int Clouds { get; set; }
    public int Visibility { get; set; }
    public double Windspeed { get; set; }
    public int WindDeg { get; set; }
    public double WindGust { get; set; }
    public double Precipitation { get; set; }
    public List<GeneralWeather> Weather { get; set; } = new List<GeneralWeather>();
}
