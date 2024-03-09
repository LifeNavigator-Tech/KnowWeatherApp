using KnowWeatherApp.Domain.Entities;
using KnowWeatherApp.Domain.Entities.Weather;

namespace KnowWeatherApp.Domain.Repositories;

public interface ICityRepository
{
    Task<IEnumerable<City>> FindCities(string cityName, string state, string country, CancellationToken cancel);
    Task<IEnumerable<City>> FindCities(string userId, CancellationToken cancel);
    Task<bool> AddCityToUser(string userId, string cityId, CancellationToken cancel);
    Task<bool> AssignReportToACityAsync(string cityId, WeatherReport item, CancellationToken cancel);
    Task<City?> GetWeatherReport(string userId, string cityId, CancellationToken cancel);
    Task<IEnumerable<City>> GetCitiesToUpdate(CancellationToken cancel);
}