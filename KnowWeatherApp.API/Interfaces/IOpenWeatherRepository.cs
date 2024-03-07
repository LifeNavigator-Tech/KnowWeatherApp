using KnowWeatherApp.API.Entities.Weather;

namespace KnowWeatherApp.API.Interfaces
{
    public interface IOpenWeatherRepository
    {
        Task<WeatherReport> GetWeatherByLocation(double lat, double lon, CancellationToken cancel);
    }
}
