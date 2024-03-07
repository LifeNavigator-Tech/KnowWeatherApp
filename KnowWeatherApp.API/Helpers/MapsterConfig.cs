using KnowWeatherApp.API.Entities;
using KnowWeatherApp.API.Models;
using KnowWeatherApp.API.Models.OpenWeather;
using Mapster;

namespace KnowWeatherApp.API.Helpers;
public static class MapsterConfig
{
    public static void RegisterMapsterConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig<CreateUserWeatherReportDto, UserWeatherReport>
                .NewConfig()
                .MapWith(report => new UserWeatherReport()
                {
                    Cities = report.Cities.Select(x => x.Adapt<City>()).ToList(),
                });

        TypeAdapterConfig<UserWeatherReport, UserWeatherReportDto>
                .NewConfig()
                .MapWith((report) => new UserWeatherReportDto()
                {
                    Cities = report.Cities.Select(x => x.Adapt<CityDto>()).ToList(),
                    WeatherReports = report.WeatherReports.Select(x => x.Adapt<WeatherReportDto>()).ToList(),
                });
    }
}
