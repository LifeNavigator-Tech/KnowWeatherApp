namespace KnowWeatherApp.API.Interfaces;

public interface IMongoRepository<T> where T : IIdentityBase
{
    public Task<List<T>> GetAsync();

    public Task<T?> GetAsync(string id);

    public Task CreateAsync(T entity);

    public Task UpdateAsync(string id, T entity);

    public Task RemoveAsync(string id);
}
