using KnowWeatherApp.Contracts;
using KnowWeatherApp.Contracts.OpenWeather;

namespace KnowWeatherApp.Common.Interfaces
{
    public interface IOpenWeatherService
    {
        Task<WeatherReportDto> GetWeatherByLocation(GetWeatherReportByLocationRequest request, CancellationToken cancel);
    }
}
