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

            return city ?? new City();
        }

        public async Task<IEnumerable<City>> GetCitiesToUpdate(CancellationToken cancel)
            => await dbContext.Cities.Where(s => s.Users.Count > 0).ToListAsync(cancel);

        public async Task<bool> ExistsAsync(string cityId, CancellationToken cancel)
            => await dbContext.Cities.AnyAsync(x => x.Id == cityId, cancel);
    }
}
