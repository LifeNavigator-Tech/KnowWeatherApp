using KnowWeatherApp.API.Models.OpenWeather;

namespace KnowWeatherApp.API.Interfaces
{
    public interface IOpenWeatherRepository
    {
        Task<WeatherReportDto> GetWeatherByLocation(double lat, double lon, CancellationToken cancel);
    }
}
