using KnowWeatherApp.Contracts.OpenWeather;

namespace KnowWeatherApp.Common.Interfaces
{
    public interface IOpenWeatherService
    {
        Task<WeatherReportDto> GetWeatherByLocation(double lat, double lon, CancellationToken cancel);
    }
}
