using KnowWeatherApp.Domain.Entities;

namespace KnowWeatherApp.Domain.Repositories;

public interface ICityRepository
{
    Task<IEnumerable<City>> FindByCityAsync(string cityName, string state, string country, CancellationToken cancel);
    Task<IEnumerable<City>> FindCitiesByUserId(string userId, CancellationToken cancel);
    Task<bool> AddCityToUser(string userId, string cityId, CancellationToken cancel);
}