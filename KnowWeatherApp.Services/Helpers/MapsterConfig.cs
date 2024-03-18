using KnowWeatherApp.Contracts;
using KnowWeatherApp.Contracts.Extensions;
using KnowWeatherApp.Contracts.Models.Triggers;
using KnowWeatherApp.Contracts.OpenWeather;
using KnowWeatherApp.Domain.Entities;
using KnowWeatherApp.Domain.Entities.Weather;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace KnowWeatherApp.Services.Helpers;
public static class MapsterConfig
{
    public static void RegisterMapsterConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig<WeatherReportDto, WeatherReport>
        .NewConfig()
            .Map(dest => dest.Current, src => src.Current.Adapt<CurrentWeatherReport>())
            .Map(dest => dest.HourlyReports, src => src.Hourly.Select(x => x.Adapt<HourlyWeatherReport>()))
            .Map(dest => dest.DailyReports, src => src.Daily.Select(x => x.Adapt<DailyWeatherReport>()).ToList());

        TypeAdapterConfig<WeatherReport, WeatherReportDto>
        .NewConfig()
            .Map(dest => dest.Current, src => src.Current.Adapt<CurrentWeatherReportDto>())
            .Map(dest => dest.Hourly, src => src.HourlyReports.Select(x => x.Adapt<HourlyWeatherReportDto>()))
            .Map(dest => dest.Daily, src => src.DailyReports.Select(x => x.Adapt<DailyWeatherReportDto>()).ToList());

        TypeAdapterConfig<City, CityDto>
            .NewConfig()
            .Map(dest => dest.WeatherReport, src => src.WeatherReport.Adapt<WeatherReportDto>())
            .TwoWays();

        TypeAdapterConfig<Trigger, TriggerSummaryDto>
            .NewConfig()
            .Map(dest => dest.City, src => $"{src.City.Name} {src.City.State}, {src.City.Country}")
            .Map(dest => dest.NotificationTypes, src => $"{string.Join(", ", src.NotificationTypes.Select(s => s.ToString()))}")
            .Map(dest => dest.EqualityType, src => src.EqualityType.GetEnumDescription())
            .Map(dest => dest.Field, src => src.Field.GetEnumDescription());
    }
}
