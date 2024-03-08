using KnowWeatherApp.API.Entities;
using KnowWeatherApp.API.Entities.Weather;

namespace KnowWeatherApp.API.Interfaces;

public interface IWeatherReportRepository
{
    Task<bool> AssignReportToACityAsync(string cityId, WeatherReport item, CancellationToken cancel);
    Task<City?> GetUserWeatherReport(string userId, string cityId, CancellationToken cancel);
    Task<IEnumerable<City>> GetCitiesToUpdate(CancellationToken cancel);
}
