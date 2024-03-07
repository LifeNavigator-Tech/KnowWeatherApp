using Microsoft.Extensions.Options;
using MongoDatabaseSettings = KnowWeatherApp.API.Configurations.MongoDatabaseSettings;
using KnowWeatherApp.API.Interfaces;
using Microsoft.Azure.Cosmos;

namespace KnowWeatherApp.API.Repositories;
public class MongoRepository<T> 
    : IMongoRepository<T> where T : IIdentityBase
{
    private CosmosClient _client;
    public Container Container;

    public MongoRepository(
        IOptions<MongoDatabaseSettings> databaseSettings)
    {
        var cosmoClient = new CosmosClient(
            databaseSettings.Value.ConnectionString);

        Container = _client.GetDatabase(databaseSettings.Value.DatabaseName).GetContainer("cities");
    }

    public async Task<List<T>> GetAsync() =>
        await Container.Get(_ => true).ToListAsync();

    public async Task<T?> GetAsync(string id) =>
        await entityCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(T entity) =>
        await entityCollection.InsertOneAsync(entity);
    
    public async Task UpdateAsync(string id, T entity) =>
        await entityCollection.ReplaceOneAsync(x => x.Id == id, entity);

    public async Task RemoveAsync(string id) =>
        await entityCollection.DeleteOneAsync(x => x.Id == id);
}

