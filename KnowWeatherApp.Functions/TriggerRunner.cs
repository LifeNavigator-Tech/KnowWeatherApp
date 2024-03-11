using System;
using KnowWeatherApp.Domain.Repositories;
using KnowWeatherApp.Services.Abstractions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace KnowWeatherApp.Functions
{
    public class TriggerRunner
    {
        private readonly ILogger _logger;
        private readonly ITriggerRepository triggerRepository;
        private readonly ITriggerAnalyzer triggerAnalyzer;

        public TriggerRunner(ILoggerFactory loggerFactory,
            ITriggerRepository triggerRepository,
            ITriggerAnalyzer triggerAnalyzer)
        {
            _logger = loggerFactory.CreateLogger<TriggerRunner>();
            this.triggerRepository = triggerRepository;
            this.triggerAnalyzer = triggerAnalyzer;
        }

        [Function("TriggerRunner")]
        public async Task Run([TimerTrigger("0 */5 * * * *"
            #if DEBUG
            , RunOnStartup=true
            #endif
            )] TimerInfo myTimer)
        {
            _logger.LogInformation($"Trigger Runner ran at: {DateTime.Now}");
            
            if (myTimer.ScheduleStatus is not null)
            {
                var triggers = await this.triggerRepository.GetTriggersToRun(CancellationToken.None);
                triggerAnalyzer.AnalyzeWeatherReport(triggers);
            }
        }
    }
}
