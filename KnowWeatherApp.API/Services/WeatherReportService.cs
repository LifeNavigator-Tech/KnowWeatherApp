using KnowWeatherApp.API.Interfaces;
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
                        .GetRequiredService<IUserWeatherReportRepository>();

                var openWeatherRepository = scope.ServiceProvider.GetRequiredService<IOpenWeatherRepository>();

                var reports = await userWeatherReportRepository.GetReportsToUpdate();

                if (reports.Count() > 0)
                {
                    foreach (var report in reports)
                    {
                        report.WeatherReports = new List<Entities.Weather.WeatherReport>();

                        foreach (var city in report.Cities)
                        {
                            var weatherReport = await openWeatherRepository.GetWeatherByLocation(city.Lat, city.Lon, CancellationToken.None);
                            weatherReport.City = city;
                            report.WeatherReports.Add(weatherReport);
                        }
                        report.Updated = DateTime.UtcNow;

                        await userWeatherReportRepository.CreateOrUpdateUserWeatherReport(report.Id, report, CancellationToken.None);
                    }
                }

                logger.LogInformation($"Weather Report Service is working. Updated {reports.Count()} reports");
            }
        }
    }
}
