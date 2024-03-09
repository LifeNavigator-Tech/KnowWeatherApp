using KnowWeatherApp.Domain.Entities;
using KnowWeatherApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KnowWeatherApp.Persistence.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly KnowWeatherDbContext dbContext;

        public CityRepository(KnowWeatherDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<City>> FindByCityAsync(string cityName, string state, string country, CancellationToken cancel)
            => await dbContext.Cities.Where(x => x.Name.StartsWith(cityName)
                        && (!string.IsNullOrEmpty(state) ? x.State.Equals(state) : true)
                        && (!string.IsNullOrEmpty(country) ? x.Country.Equals(country) : true))
                         .ToListAsync(cancel);

        public async Task<IEnumerable<City>> FindCitiesByUserId(string userId, CancellationToken cancel)
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
    }
}
