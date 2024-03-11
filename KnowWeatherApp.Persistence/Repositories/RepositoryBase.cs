using KnowWeatherApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KnowWeatherApp.Persistence.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        public KnowWeatherDbContext RepositoryContext { get; set; }

        public RepositoryBase(KnowWeatherDbContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
        }
        public async Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> expression, CancellationToken cancel)
        {
            return await this.RepositoryContext.Set<T>()
                .Where(expression)
                .AsNoTracking()
                .ToListAsync(cancel);
        }
        public void Create(T entity)
        {
            this.RepositoryContext.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            this.RepositoryContext.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            this.RepositoryContext.Set<T>().Remove(entity);
        }

        public async Task SaveChangesAsync(CancellationToken cancel)
        {
            await this.RepositoryContext.SaveChangesAsync(cancel);
        }
    }
}
