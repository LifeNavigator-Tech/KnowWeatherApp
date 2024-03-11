using KnowWeatherApp.Domain.Entities;

namespace KnowWeatherApp.Domain.Repositories
{
    public interface ITriggerRepository : IRepositoryBase<Trigger>
    {
        Task<IEnumerable<Trigger>> GetTriggersToRun(CancellationToken cancel);
    }
}
