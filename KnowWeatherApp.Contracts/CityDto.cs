using KnowWeatherApp.Contracts.OpenWeather;

namespace KnowWeatherApp.Contracts;

public class CityDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public double Lon { get; set; }
    public double Lat { get; set; }
    public WeatherReportDto WeatherReport { get; set; }
}
