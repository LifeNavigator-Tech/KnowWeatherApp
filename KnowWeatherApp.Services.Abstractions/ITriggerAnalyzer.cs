using KnowWeatherApp.Domain.Entities;

namespace KnowWeatherApp.Services.Abstractions
{
    public interface ITriggerAnalyzer
    {
        void AnalyzeWeatherReport(IEnumerable<Trigger> triggers);
    }
}
