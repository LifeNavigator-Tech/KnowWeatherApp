using KnowWeatherApp.Contracts;

namespace KnowWeatherApp.WebUI.Interfaces
{
    public interface IKnowWeatherCitiesService
    {
        Task<IEnumerable<CityDto>> GetCities(SearchCityRequestDto searchCityRequest);
    }
}
