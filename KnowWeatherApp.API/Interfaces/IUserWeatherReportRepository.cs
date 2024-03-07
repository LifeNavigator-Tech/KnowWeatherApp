using KnowWeatherApp.API.Entities;

namespace KnowWeatherApp.API.Interfaces;

public interface IUserWeatherReportRepository
{
    Task CreateOrUpdateUserWeatherReport(string userId, UserWeatherReport item, CancellationToken cancel);
    Task<UserWeatherReport?> GetUserWeatherReport(string userId, CancellationToken cancel);
    Task<IEnumerable<UserWeatherReport>> GetReportsToUpdate();
}
