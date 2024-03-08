using KnowWeatherApp.API.Entities;
using KnowWeatherApp.API.Entities.Weather;
using KnowWeatherApp.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KnowWeatherApp.API.Repositories
{
    public class UserWeatherReportRepository : IWeatherReportRepository
    {
        private readonly KnowWeatherDbContext context;
        private readonly ILogger<UserWeatherReportRepository> logger;

        public UserWeatherReportRepository(
            KnowWeatherDbContext context,
            ILogger<UserWeatherReportRepository> logger)
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

            var weatherEntity = await context.WeatherReports.FirstOrDefaultAsync(x => x.CityId == cityId);
            if (weatherEntity == null)
            {
                await this.context.WeatherReports.AddAsync(weatherReport);
            }
            else
            {
                weatherEntity = weatherReport;
            }

            await context.SaveChangesAsync(cancel);
            return true;
        }

        public async Task<IEnumerable<City>> GetCitiesToUpdate(CancellationToken cancel)
            => await context.Cities.Where(s => s.Users.Count > 0).ToListAsync(cancel);

        public async Task<IEnumerable<City>> GetUserWeatherReport(string userId, CancellationToken cancel)
            => await context.Cities.Where(x => x.Users.Select(u => u.Id).Contains(userId))
            .Include(x => x.WeatherReport)
            .ToListAsync(cancel);
    }
}
