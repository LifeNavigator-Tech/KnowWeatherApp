using KnowWeatherApp.Domain.Entities;
using KnowWeatherApp.Domain.Entities.Weather;
using KnowWeatherApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KnowWeatherApp.Persistence.Repositories
{
    public class CitytRepository : ICityRepository
    {
        private readonly KnowWeatherDbContext dbContext;

        public CitytRepository(KnowWeatherDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<City>> FindCities(string cityName, string state, string country, CancellationToken cancel)
            => await dbContext.Cities.Where(x => x.Name.StartsWith(cityName)
                        && (!string.IsNullOrEmpty(state) ? x.State.Equals(state) : true)
                        && (!string.IsNullOrEmpty(country) ? x.Country.Equals(country) : true))
                         .ToListAsync(cancel);

        public async Task<IEnumerable<City>> FindCities(string userId, CancellationToken cancel)
            => await dbContext.Cities.Where(x => x.Users.Select(u => u.Id).Contains(userId))
                         .ToListAsync(cancel);

        public async Task<bool> AddCityToUser(string userId, string cityId, CancellationToken cancel)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.Id == userId);
            var city = dbContext.Cities.FirstOrDefault(x => x.Id == cityId);

            if (user != null && city != null)
            {
                user.Cities.Add(city);
                await dbContext.SaveChangesAsync(cancel);
                return true;
            }

            return false;
        }

        public async Task<bool> AssignReportToACityAsync(string cityId, WeatherReport weatherReport, CancellationToken cancel)
        {
            var cityExists = await dbContext.Cities.AnyAsync(x => x.Id == cityId);
            if (!cityExists)
            {
                return false;
            }

            var weatherEntity = await dbContext.WeatherReports.Include(x => x.City).FirstOrDefaultAsync(x => x.CityId == cityId);

            if (weatherEntity == null)
            {
                await dbContext.WeatherReports.AddAsync(weatherReport);
            }
            else
            {
                weatherEntity.DailyReports = weatherReport.DailyReports;
                weatherEntity.HourlyReports = weatherReport.HourlyReports;
                weatherEntity.Current = weatherReport.Current;
            }


            await dbContext.SaveChangesAsync(cancel);
            return true;
        }

        public async Task<IEnumerable<City>> GetCitiesToUpdate(CancellationToken cancel)
            => await dbContext.Cities.Where(s => s.Users.Count > 0).ToListAsync(cancel);

        public async Task<City?> GetWeatherReport(string userId, string cityId, CancellationToken cancel)
            => await dbContext.Cities
                             .Include(x => x.WeatherReport)
                                .ThenInclude(x => x.DailyReports)
                             .Include(x => x.WeatherReport)
                                .ThenInclude(x => x.HourlyReports)
                            .FirstOrDefaultAsync(x => x.Users.Select(u => u.Id).Contains(userId)
                                                    && x.Id == cityId, cancel);
    }
}
