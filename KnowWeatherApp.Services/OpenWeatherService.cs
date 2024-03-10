using KnowWeatherApp.Common.Interfaces;
using KnowWeatherApp.Contracts;
using KnowWeatherApp.Contracts.OpenWeather;
using KnowWeatherApp.Services.Configurations;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace KnowWeatherApp.Common.Repositories
{
    public class OpenWeatherService : IOpenWeatherService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IOptions<OpenWeatherSettings> options;

        public OpenWeatherService(IHttpClientFactory httpClientFactory, IOptions<OpenWeatherSettings> options)
        {
            this.httpClientFactory = httpClientFactory;
            this.options = options;
        }

        public async Task<WeatherReportDto> GetWeatherByLocation(GetWeatherReportByLocationRequest request, CancellationToken cancel)
        {
            using var httpclient = httpClientFactory.CreateClient("WeatherAPI");

            var weatherUrlQuery = $"?lat={request.Lat}&lon={request.Lon}&appid={options.Value.APIKEY}&units=imperial";
            var response = await httpclient.GetAsync(weatherUrlQuery, cancel);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var weatherReport = JsonSerializer.Deserialize<WeatherReportDto>(
                content,
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return weatherReport ?? new WeatherReportDto();
        }
    }
}
