using KnowWeatherApp.Domain.Entities.Weather;

namespace KnowWeatherApp.Domain.Repositories
{
    public interface IWeatherReportRepository
    {
        Task<WeatherReport> GetWeatherReport(string userId, string cityId, CancellationToken cancel);
        Task AssignReportToACityAsync(string cityId, WeatherReport weatherReport, CancellationToken cancel);
    }
}
