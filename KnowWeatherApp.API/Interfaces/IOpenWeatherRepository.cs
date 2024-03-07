using KnowWeatherApp.API.Entities;
using KnowWeatherApp.API.Entities.Weather;

namespace KnowWeatherApp.API.Interfaces
{
    public interface IOpenWeatherRepository
    {
        Task<WeatherReport> GetWeatherByCity(Location location, CancellationToken cancel);
    }
}
