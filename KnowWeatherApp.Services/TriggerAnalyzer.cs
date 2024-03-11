using KnowWeatherApp.Domain.Entities;
using KnowWeatherApp.Domain.Entities.Weather;
using KnowWeatherApp.Domain.Enums;
using KnowWeatherApp.Services.Abstractions;

namespace KnowWeatherApp.Services
{
    public class TriggerAnalyzer : ITriggerAnalyzer
    {
        private readonly IAzureQueueService azureQueueService;

        public TriggerAnalyzer(IAzureQueueService azureQueueService)
        {
            this.azureQueueService = azureQueueService;
        }
        public void AnalyzeWeatherReport(IEnumerable<Trigger> triggers)
        {
            foreach (var trigger in triggers)
            {
                if (trigger?.City?.WeatherReport == null) continue;

                var result = AnalyzeDailyWeatherReport(trigger, trigger.City.WeatherReport.DailyReports);
            }
        }

        private DailyWeatherReport? AnalyzeDailyWeatherReport(Trigger trigger, List<DailyWeatherReport> dailyReports)
        {
            Func<DailyWeatherReport, bool> filter = (report) => false;

            var gte = trigger.EqualityType == EqualityType.GreaterOrEqual;

            switch (trigger.Field)
            {
                case WeatherFieldType.Pressure:
                    filter = (x => Compare(x.Pressure, gte, trigger.Threshold));
                    break;
                case WeatherFieldType.DewPoint:
                    filter = (x => Compare(x.DewPoint, gte, trigger.Threshold));
                    break;
                case WeatherFieldType.WindSpeed:
                    filter = (x => Compare(x.WindSpeed, gte, trigger.Threshold));
                    break;
                case WeatherFieldType.Uvi:
                    filter = (x => Compare(x.Uvi, gte, trigger.Threshold));
                    break;
                case WeatherFieldType.Humidity:
                    filter = (x => Compare(x.Humidity, gte, trigger.Threshold));
                    break;
                case WeatherFieldType.WindGust:
                    filter = (x => Compare(x.WindGust, gte, trigger.Threshold));
                    break;
                case WeatherFieldType.TempMorning:
                    filter = (x => Compare(x.Temp.Morning, gte, trigger.Threshold));
                    break;
                case WeatherFieldType.TempEvening:
                    filter = (x => Compare(x.Temp.Evening, gte, trigger.Threshold));
                    break;
                case WeatherFieldType.TempNight:
                    filter = (x => Compare(x.Temp.Night, gte, trigger.Threshold));
                    break;
                case WeatherFieldType.TempDay:
                    filter = (x => Compare(x.Temp.Day, gte, trigger.Threshold));
                    break;
                case WeatherFieldType.TempMin:
                    filter = (x => Compare(x.Temp.Min, gte, trigger.Threshold));
                    break;
                case WeatherFieldType.TempMax:
                    filter = (x => Compare(x.Temp.Max, gte, trigger.Threshold));
                    break;
                case WeatherFieldType.FeelsLikeMorning:
                    filter = (x => Compare(x.FeelsLike.Morning, gte, trigger.Threshold));
                    break;
                case WeatherFieldType.FeelsLikeEvening:
                    filter = (x => Compare(x.FeelsLike.Evening, gte, trigger.Threshold));
                    break;
                case WeatherFieldType.FeelsLikeNight:
                    filter = (x => Compare(x.FeelsLike.Night, gte, trigger.Threshold));
                    break;
                case WeatherFieldType.FeelsLikeDay:
                    filter = (x => Compare(x.FeelsLike.Day, gte, trigger.Threshold));
                    break;
                case WeatherFieldType.Clouds:
                    filter = (x => Compare(x.Clouds, gte, trigger.Threshold));
                    break;
                case WeatherFieldType.Precipitation:
                    filter = (x => Compare(x.Precipitation, gte, trigger.Threshold));
                    break;
                case WeatherFieldType.Rain:
                    filter = (x => Compare(x.Rain, gte, trigger.Threshold));
                    break;
                case WeatherFieldType.SunRise:
                    filter = (x => Compare(x.Sunrise, gte, trigger.Threshold));
                    break;
                case WeatherFieldType.SunSet:
                    filter = (x => Compare(x.Sunset, gte, trigger.Threshold));
                    break;
                default:
                    break;
            }

            return dailyReports.FirstOrDefault(filter);
        }

        private static bool Compare(int value, bool gte, string threshold)
        {
            int thresholdInt = int.Parse(threshold);
            return gte ?
                   value >= thresholdInt :
                   value <= thresholdInt;
        }

        private static bool Compare(double value, bool gte, string threshold)
        {
            double thresholdDouble = double.Parse(threshold);
            return gte ?
                   value >= thresholdDouble :
                   value <= thresholdDouble;
        }

        private static bool Compare(DateTime value, bool gte, string threshold)
        {
            DateTime thresholdDateTime = DateTime.Parse(threshold);
            return gte ?
                   value >= thresholdDateTime :
                   value <= thresholdDateTime;
        }
    }
}
