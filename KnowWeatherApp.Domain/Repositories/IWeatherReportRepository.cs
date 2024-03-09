using KnowWeatherApp.Domain.Entities;
using KnowWeatherApp.Domain.Entities.Weather;

namespace KnowWeatherApp.Domain.Repositories;

public interface IWeatherReportRepository
{
    Task<bool> AssignReportToACityAsync(string cityId, WeatherReport item, CancellationToken cancel);
    Task<City?> GetUserWeatherReport(string userId, string cityId, CancellationToken cancel);
    Task<IEnumerable<City>> GetCitiesToUpdate(CancellationToken cancel);
}
