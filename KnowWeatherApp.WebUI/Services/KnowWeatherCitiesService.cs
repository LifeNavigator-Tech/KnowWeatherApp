using Cysharp.Web;
using KnowWeatherApp.Contracts;
using KnowWeatherApp.WebUI.Interfaces;
using System.Text;
using System.Text.Json;

namespace KnowWeatherApp.WebUI.Services
{
    public class KnowWeatherCitiesService: IKnowWeatherCitiesService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly JsonSerializerOptions options;

        public KnowWeatherCitiesService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
            this.options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public async Task<IEnumerable<CityDto>> GetCities(SearchCityRequestDto searchCityRequest)
        {
            var httpClient = this.httpClientFactory.CreateClient("KnowWeatherAppAPI");
            var q = WebSerializer.ToQueryString(searchCityRequest);
            using var response = await httpClient.GetAsync("cities?" + q);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<CityDto>>(responseContent, this.options);

            return result ?? new List<CityDto>();
        }
    }
}
