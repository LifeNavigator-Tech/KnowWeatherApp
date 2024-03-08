using KnowWeatherApp.API.Configurations;
using KnowWeatherApp.API.Entities.Weather;
using KnowWeatherApp.API.Interfaces;
using KnowWeatherApp.API.Models.OpenWeather;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace KnowWeatherApp.API.Repositories
{
    public class OpenWeatherRepository : IOpenWeatherRepository
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IOptions<OpenWeatherSettings> options;

        public OpenWeatherRepository(IHttpClientFactory httpClientFactory, IOptions<OpenWeatherSettings> options)
        {
            this.httpClientFactory = httpClientFactory;
            this.options = options;
        }

        public async Task<WeatherReportDto> GetWeatherByLocation(double lat, double lon, CancellationToken cancel)
        {
            using var httpclient = this.httpClientFactory.CreateClient("WeatherAPI");

            var weatherUrlQuery = $"?lat={lat}&lon={lon}&appid={this.options.Value.APIKEY}&units=imperial";
            var response = await httpclient.GetAsync(weatherUrlQuery, cancel);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var weatherReport = JsonSerializer.Deserialize<WeatherReportDto>(
                content,
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return weatherReport;
        }
    }
}
