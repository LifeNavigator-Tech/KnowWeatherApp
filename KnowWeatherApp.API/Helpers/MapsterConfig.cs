using KnowWeatherApp.Contracts;
using KnowWeatherApp.Contracts.OpenWeather;
using KnowWeatherApp.Domain.Entities;
using KnowWeatherApp.Domain.Entities.Weather;
using Mapster;

namespace KnowWeatherApp.API.Helpers;
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
    }
}
