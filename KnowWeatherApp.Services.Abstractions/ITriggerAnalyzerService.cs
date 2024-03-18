using KnowWeatherApp.Contracts;
using KnowWeatherApp.Domain.Entities;

namespace KnowWeatherApp.Services.Abstractions
{
    public interface ITriggerAnalyzerService
    {
        IEnumerable<NotificationDto> AnalyzeWeatherReport(IEnumerable<Trigger> triggers);
    }
}
