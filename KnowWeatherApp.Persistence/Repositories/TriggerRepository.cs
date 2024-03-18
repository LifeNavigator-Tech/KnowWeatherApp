using KnowWeatherApp.Domain.Entities;
using KnowWeatherApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KnowWeatherApp.Persistence.Repositories
{
    public class TriggerRepository : RepositoryBase<Trigger>, ITriggerRepository
    {
        public TriggerRepository(KnowWeatherDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Trigger>> GetUsersTriggers(string userId, CancellationToken cancel)
            => await RepositoryContext.Triggers
                                    .Include(x => x.City)
                                    .Where(x => x.UserId == userId)
                                    .ToListAsync(cancel);

        public async Task<IEnumerable<Trigger>> GetTriggersToRun(CancellationToken cancel)
            => await RepositoryContext.Triggers
                        .Where(x => x.TimeToNotify.Hour == DateTime.UtcNow.Hour)
                        .Where(x => x.IsActive)
                        .Include(x => x.User)
                        .Include(x => x.City)
                        .ThenInclude(x => x.WeatherReport)
                        .ToListAsync(cancel);
    }
}
