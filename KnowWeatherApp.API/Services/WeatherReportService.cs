using KnowWeatherApp.API.Entities.Weather;
using KnowWeatherApp.API.Interfaces;
using Mapster;
using Microsoft.Extensions.Options;
using WeatherPass.FunctionApp.Helpers;

namespace KnowWeatherApp.API.Services
{
    public class WeatherReportService : BackgroundService
    {
        private readonly ILogger<WeatherReportService> logger;
        private readonly IOptionsMonitor<OpenWeatherSettings> options;
        private readonly IServiceProvider services;

        public WeatherReportService(
            ILogger<WeatherReportService> logger,
            IOptionsMonitor<OpenWeatherSettings> options,
            IServiceProvider services)
        {
            this.logger = logger;
            this.options = options;
            this.services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken cancel)
        {
            logger.LogInformation("Weather Report Service is running.");

            await GetWeatherReportsAsync();

            using PeriodicTimer timer = new(TimeSpan.FromMinutes(this.options.CurrentValue.UpdateIntervalInMinutes));

            try
            {
                while (await timer.WaitForNextTickAsync(cancel))
                {
                    await GetWeatherReportsAsync();
                }
            }
            catch (OperationCanceledException)
            {
                logger.LogInformation("Weather Report Service is stopping.");
            }
        }

        private async Task GetWeatherReportsAsync()
        {
            using (var scope = this.services.CreateScope())
            {
                var userWeatherReportRepository =
                    scope.ServiceProvider
                        .GetRequiredService<IWeatherReportRepository>();

                var openWeatherRepository = scope.ServiceProvider.GetRequiredService<IOpenWeatherRepository>();

                var cityRepository = scope.ServiceProvider.GetRequiredService<ICityRepository>();

                var cities = await userWeatherReportRepository.GetCitiesToUpdate(CancellationToken.None);

                foreach (var city in cities)
                {
                    var modelWeather = await openWeatherRepository.GetWeatherByLocation(city.Lat, city.Lon, CancellationToken.None);
                    var report = modelWeather.Adapt<WeatherReport>();
                    await userWeatherReportRepository.AssignReportToACityAsync(city.Id, report, CancellationToken.None);
                }

                logger.LogInformation($"Weather Report Service is working. Updated {cities.Count()} cities");
            }
        }
    }
}
