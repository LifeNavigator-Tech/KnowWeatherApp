using KnowWeatherApp.Contracts;
using KnowWeatherApp.Domain.Entities;

namespace KnowWeatherApp.Domain.Repositories;

public interface ICityRepository : IRepositoryBase<City>
{
    Task<IEnumerable<City>> FindCities(SearchCityRequestDto request, CancellationToken cancel);
    Task<IEnumerable<City>> FindCities(string userId, CancellationToken cancel);
    Task<City> AddCityToUser(string userId, string cityId);
    Task<IEnumerable<City>> GetCitiesToUpdate(CancellationToken cancel);
    Task<bool> ExistsAsync(string cityId, CancellationToken cancel);
}