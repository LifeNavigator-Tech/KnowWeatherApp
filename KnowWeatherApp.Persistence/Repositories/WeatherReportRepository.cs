
using KnowWeatherApp.Domain.Entities;
using KnowWeatherApp.Domain.Entities.Weather;
using KnowWeatherApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KnowWeatherApp.Persistence.Repositories
{
    public class WeatherReportRepository : IWeatherReportRepository
    {
        private readonly KnowWeatherDbContext context;
        private readonly ILogger<WeatherReportRepository> logger;

        public WeatherReportRepository(
            KnowWeatherDbContext context,
            ILogger<WeatherReportRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<bool> AssignReportToACityAsync(string cityId, WeatherReport weatherReport, CancellationToken cancel)
        {
            var cityExists = await context.Cities.AnyAsync(x => x.Id == cityId);
            if (!cityExists)
            {
                return false;
            }

            var weatherEntity = await context.WeatherReports.Include(x => x.City).FirstOrDefaultAsync(x => x.CityId == cityId);

            if (weatherEntity == null)
            {
                await context.WeatherReports.AddAsync(weatherReport);
            }
            else
            {
                weatherEntity.DailyReports = weatherReport.DailyReports;
                weatherEntity.HourlyReports = weatherReport.HourlyReports;
                weatherEntity.Current = weatherReport.Current;
            }


            await context.SaveChangesAsync(cancel);
            return true;
        }

        public async Task<IEnumerable<City>> GetCitiesToUpdate(CancellationToken cancel)
            => await context.Cities.Where(s => s.Users.Count > 0).ToListAsync(cancel);

        public async Task<City?> GetUserWeatherReport(string userId, string cityId, CancellationToken cancel)
            => await context.Cities
                             .Include(x => x.WeatherReport)
                                .ThenInclude(x => x.DailyReports)
                             .Include(x => x.WeatherReport)
                                .ThenInclude(x => x.HourlyReports)
                            .FirstOrDefaultAsync(x => x.Users.Select(u => u.Id).Contains(userId)
                                                    && x.Id == cityId, cancel);
    }
}
