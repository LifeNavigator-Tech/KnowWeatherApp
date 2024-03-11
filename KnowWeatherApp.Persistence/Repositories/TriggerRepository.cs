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

        public async Task<IEnumerable<Trigger>> GetTriggersToRun(CancellationToken cancel)
            => await RepositoryContext.Triggers
                        .Where(x => x.TimeToNotify.Hour == DateTime.UtcNow.Hour)
                        .Include(x => x.User)
                        .Include(x => x.City)
                        .ThenInclude(x => x.WeatherReport)
                        .ToListAsync(cancel);
    }
}
