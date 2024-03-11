using KnowWeatherApp.Domain.Entities.Weather;
using KnowWeatherApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KnowWeatherApp.Persistence.Repositories
{
    public class WeatherReportRepository : IWeatherReportRepository
    {
        private readonly KnowWeatherDbContext dbContext;

        public WeatherReportRepository(KnowWeatherDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<WeatherReport> GetWeatherReport(string userId, string cityId, CancellationToken cancel)
        {
            var result = await this.dbContext.WeatherReports
                             .Where(x => x.City.Users
                                           .Select(u => u.Id)
                                           .Contains(userId) && x.CityId == cityId)
                             .Include(x => x.DailyReports)
                             .Include(x => x.HourlyReports)
                             .FirstOrDefaultAsync(cancel);

            return result ?? new WeatherReport();
        }

        public async Task AssignReportToACityAsync(string cityId, WeatherReport weatherReport, CancellationToken cancel)
        {
            var weatherEntity = await dbContext.WeatherReports
                .Include(x => x.City)
                .FirstOrDefaultAsync(x => x.CityId == cityId);

            if (weatherEntity == null)
            {
                weatherReport.CityId = cityId;
                weatherReport.Created = DateTime.UtcNow;
                weatherReport.Updated = DateTime.UtcNow;
                await dbContext.WeatherReports.AddAsync(weatherReport);
            }
            else
            {
                weatherEntity.Updated = DateTime.UtcNow;
                weatherEntity.DailyReports = weatherReport.DailyReports;
                weatherEntity.HourlyReports = weatherReport.HourlyReports;
                weatherEntity.Current = weatherReport.Current;
            }

            await dbContext.SaveChangesAsync(cancel);
        }
    }
}
