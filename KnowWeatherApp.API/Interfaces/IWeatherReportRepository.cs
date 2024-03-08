using KnowWeatherApp.API.Entities;
using KnowWeatherApp.API.Entities.Weather;

namespace KnowWeatherApp.API.Interfaces;

public interface IWeatherReportRepository
{
    Task<bool> AssignReportToACityAsync(string cityId, WeatherReport item, CancellationToken cancel);
    Task<IEnumerable<City>> GetUserWeatherReport(string userId, CancellationToken cancel);
    Task<IEnumerable<City>> GetCitiesToUpdate(CancellationToken cancel);
}
