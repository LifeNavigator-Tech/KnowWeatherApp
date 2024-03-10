using KnowWeatherApp.Common.Interfaces;
using KnowWeatherApp.Contracts;
using KnowWeatherApp.Domain.Entities.Weather;
using KnowWeatherApp.Domain.Repositories;
using Mapster;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace KnowWeatherApp.Functions
{
    public class UpdateReportsRunner
    {
        private readonly ILogger logger;
        private readonly ICityRepository cityRepository;
        private readonly IWeatherReportRepository weatherReportRepository;
        private readonly IOpenWeatherService openWeatherService;

        public UpdateReportsRunner(
            ILoggerFactory loggerFactory,
            ICityRepository cityRepository,
            IWeatherReportRepository weatherReportRepository,
            IOpenWeatherService openWeatherService)
        {
            this.logger = loggerFactory.CreateLogger<UpdateReportsRunner>();
            this.cityRepository = cityRepository;
            this.weatherReportRepository = weatherReportRepository;
            this.openWeatherService = openWeatherService;
        }

        [Function("UpdateReportsRunner")]
        public async Task Run([TimerTrigger("0 */5 * * * *"
            #if DEBUG
            , RunOnStartup=true
            #endif
            )] TimerInfo myTimer)
        {
            logger.LogInformation($"Weather Service Runner run at: {DateTime.Now}");

            if (myTimer.ScheduleStatus is not null)
            {
                var cities = await cityRepository.GetCitiesToUpdate(CancellationToken.None);

                foreach (var city in cities)
                {
                    var request = new GetWeatherReportByLocationRequest(city.Lat, city.Lon);
                    var modelWeather = await openWeatherService.GetWeatherByLocation(request, CancellationToken.None);
                    var report = modelWeather.Adapt<WeatherReport>();
                    await weatherReportRepository.AssignReportToACityAsync(city.Id, report, CancellationToken.None);
                }

                logger.LogInformation($"Weather Report Service is working. Updated {cities.Count()} cities");
            }
        }
    }
}
