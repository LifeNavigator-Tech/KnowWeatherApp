using System.Linq.Expressions;

namespace KnowWeatherApp.Domain.Repositories
{
    public interface IRepositoryBase<T>
    {
        Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> expression, CancellationToken cancel);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync(CancellationToken cancel);
    }
}
