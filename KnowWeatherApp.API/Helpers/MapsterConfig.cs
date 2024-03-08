using KnowWeatherApp.API.Entities;
using KnowWeatherApp.API.Entities.Weather;
using KnowWeatherApp.API.Models;
using KnowWeatherApp.API.Models.OpenWeather;
using Mapster;

namespace KnowWeatherApp.API.Helpers;
public static class MapsterConfig
{
    public static void RegisterMapsterConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig<WeatherReportDto, WeatherReport>
        .NewConfig()
        .MapWith((report) => new WeatherReport()
        {
            Lat = report.Lat,
            Lon = report.Lon,
            Current = report.Current.Adapt<CurrentWeatherReport>(),
            TimeZone = report.TimeZone,
            TimeZoneOffset = report.TimeZoneOffset,
            HourlyReports = report.Hourly.Select(x => x.Adapt<HourlyWeatherReport>()).ToList(),
            DailyReports = report.Hourly.Select(x => x.Adapt<DailyWeatherReport>()).ToList(),
        });

        TypeAdapterConfig<WeatherReport, WeatherReportDto>
            .NewConfig()
            .MapWith((report) => new WeatherReportDto()
            {
                Lat = report.Lat,
                Lon = report.Lon,
                Current = report.Current.Adapt<CurrentWeatherReportDto>(),
                TimeZone = report.TimeZone,
                TimeZoneOffset = report.TimeZoneOffset,
                Hourly = report.HourlyReports.Select(x => x.Adapt<HourlyWeatherReportDto>()).ToList(),
                Daily = report.DailyReports.Select(x => x.Adapt<DailyWeatherReportDto>()).ToList(),
            });

        TypeAdapterConfig<City, CityDto>.NewConfig()
            .MapWith(city => new CityDto()
            {
                WeatherReport = city.Adapt<WeatherReportDto>(),
            });
    }
}
