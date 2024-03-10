using KnowWeatherApp.Contracts;
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

        public async Task<IEnumerable<City>> FindCities(SearchCityRequestDto request, CancellationToken cancel)
            => await dbContext.Cities.Where(x => x.Name.StartsWith(request.City)
                        && (!string.IsNullOrEmpty(request.State) ? x.State.Equals(request.State) : true)
                        && (!string.IsNullOrEmpty(request.Country) ? x.Country.Equals(request.Country) : true))
                         .ToListAsync(cancel);

        public async Task<IEnumerable<City>> FindCities(string userId, CancellationToken cancel)
            => await dbContext.Cities.Where(x => x.Users.Select(u => u.Id).Contains(userId))
                         .ToListAsync(cancel);

        public async Task<City> AddCityToUser(string userId, string cityId, CancellationToken cancel)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.Id == userId);
            var city = dbContext.Cities.Include(x => x.WeatherReport).FirstOrDefault(x => x.Id == cityId);

            if (user != null && city != null)
            {
                user.Cities.Add(city);
                await dbContext.SaveChangesAsync(cancel);
            }

            return city;
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
                weatherReport.CityId = cityId;
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
