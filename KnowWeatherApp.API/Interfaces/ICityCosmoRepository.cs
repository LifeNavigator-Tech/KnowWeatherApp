using KnowWeatherApp.API.Entities;
using KnowWeatherApp.API.Models;

namespace KnowWeatherApp.API.Interfaces;

public interface ICityCosmoRepository
{
    Task<IEnumerable<City>> FindByCityAsync(RequestCityDto cityRequest);
}