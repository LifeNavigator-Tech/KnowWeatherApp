using KnowWeatherApp.API.Entities;
using KnowWeatherApp.API.Models;

namespace KnowWeatherApp.API.Interfaces;

public interface ICityRepository
{
    Task<IEnumerable<City>> FindByCityAsync(SearchCityRequestDto cityRequest);
    Task<IEnumerable<City>> FindCitiesByUserId(string userId);
    Task<bool> AddCityToUser(string userId, string cityId);
}