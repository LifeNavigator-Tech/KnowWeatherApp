using KnowWeatherApp.API.Entities;
using KnowWeatherApp.API.Models;

namespace KnowWeatherApp.API.Interfaces;

public interface ICityRepository
{
    Task<IEnumerable<City>> FindByCityAsync(SearchCityRequestDto cityRequest, CancellationToken cancel);
    Task<IEnumerable<City>> FindCitiesByUserId(string userId, CancellationToken cancel);
    Task<bool> AddCityToUser(string userId, string cityId, CancellationToken cancel);
}