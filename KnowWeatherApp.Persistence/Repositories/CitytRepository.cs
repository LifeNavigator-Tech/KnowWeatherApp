using KnowWeatherApp.Contracts;
using KnowWeatherApp.Domain.Entities;
using KnowWeatherApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KnowWeatherApp.Persistence.Repositories
{
    public class CitytRepository : RepositoryBase<City>, ICityRepository
    {

        public CitytRepository(KnowWeatherDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<City>> FindCities(SearchCityRequestDto request, CancellationToken cancel)
            => await this.FindByCondition(x => x.Name.StartsWith(request.City)
                            && (!string.IsNullOrEmpty(request.State) ? x.State.Equals(request.State) : true)
                            && (!string.IsNullOrEmpty(request.Country) ? x.Country.Equals(request.Country) : true),
                        cancel);

        public async Task<IEnumerable<City>> FindCities(string userId, CancellationToken cancel)
            => await this.FindByCondition(x => x.Users.Select(u => u.Id).Contains(userId), 
                          cancel);

        public async Task<City> AddCityToUser(string userId, string cityId)
        {
            var user = await RepositoryContext.Users
                            .FirstOrDefaultAsync(x => x.Id == userId);

            var city = await RepositoryContext.Cities
                            .Include(x => x.WeatherReport)
                            .FirstOrDefaultAsync(x => x.Id == cityId);

            if (user != null && city != null)
            {
                user.Cities.Add(city);
            }

            return city ?? new City();
        }

        public async Task<IEnumerable<City>> GetCitiesToUpdate(CancellationToken cancel)
            => await this.FindByCondition(s =>
                s.Users.Count > 0
                && (s.WeatherReport == null || s.WeatherReport.Updated < DateTime.UtcNow.AddHours(-4)), cancel);

        public async Task<bool> ExistsAsync(string cityId, CancellationToken cancel)
            => await RepositoryContext.Cities.AnyAsync(x => x.Id == cityId, cancel);
    }
}
