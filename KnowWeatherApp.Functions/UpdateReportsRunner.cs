using KnowWeatherApp.Common.Interfaces;
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
        private readonly IOpenWeatherService openWeatherService;

        public UpdateReportsRunner(
            ILoggerFactory loggerFactory,
            ICityRepository cityRepository,
            IOpenWeatherService openWeatherService)
        {
            logger = loggerFactory.CreateLogger<UpdateReportsRunner>();
            this.cityRepository = cityRepository;
            this.openWeatherService = openWeatherService;
        }

        [Function("UpdateReportsRunner")]
        public async Task Run([TimerTrigger("0 */5 * * * *"
            #if DEBUG
            , RunOnStartup=true
            #endif
            )] TimerInfo myTimer)
        {
            logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            
            if (myTimer.ScheduleStatus is not null)
            {
                var cities = await cityRepository.GetCitiesToUpdate(CancellationToken.None);

                foreach (var city in cities)
                {
                    var modelWeather = await openWeatherService.GetWeatherByLocation(city.Lat, city.Lon, CancellationToken.None);
                    var report = modelWeather.Adapt<WeatherReport>();
                    await cityRepository.AssignReportToACityAsync(city.Id, report, CancellationToken.None);
                }

                logger.LogInformation($"Weather Report Service is working. Updated {cities.Count()} cities");
            }
        }
    }
}
