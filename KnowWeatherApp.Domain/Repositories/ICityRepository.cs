using KnowWeatherApp.Contracts;
using KnowWeatherApp.Domain.Entities;
using KnowWeatherApp.Domain.Entities.Weather;

namespace KnowWeatherApp.Domain.Repositories;

public interface ICityRepository
{
    Task<IEnumerable<City>> FindCities(SearchCityRequestDto request, CancellationToken cancel);
    Task<IEnumerable<City>> FindCities(string userId, CancellationToken cancel);
    Task<City> AddCityToUser(string userId, string cityId, CancellationToken cancel);
    Task<bool> AssignReportToACityAsync(string cityId, WeatherReport item, CancellationToken cancel);
    Task<City?> GetWeatherReport(string userId, string cityId, CancellationToken cancel);
    Task<IEnumerable<City>> GetCitiesToUpdate(CancellationToken cancel);
}