using KnowWeatherApp.API.Configurations;
using KnowWeatherApp.API.Entities;
using KnowWeatherApp.API.Interfaces;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Options;

namespace KnowWeatherApp.API.Repositories
{
    public class UserWeatherReportRepository : IUserWeatherReportRepository
    {
        private Container container;
        private readonly ILogger<UserWeatherReportRepository> logger;

        public UserWeatherReportRepository(
            IOptions<MongoDatabaseSettings> databaseSettings,
            ILogger<UserWeatherReportRepository> logger)
        {
            var cosmoClient = new CosmosClient(databaseSettings.Value.ConnectionString, new CosmosClientOptions()
            {
                SerializerOptions = new CosmosSerializationOptions()
                {
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase,
                }
            });
            this.container = cosmoClient.GetDatabase(databaseSettings.Value.DatabaseName).GetContainer("user-weather-reports");
            this.logger = logger;
        }
        public async Task CreateOrUpdateUserWeatherReport(string userId, UserWeatherReport item, CancellationToken cancel)
        {
            item.Id = userId;

            try
            {
                await container.UpsertItemAsync(item: item, cancellationToken: cancel);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Unable to update user report. User id: {userId}");
                throw;
            }
        }

        public async Task<UserWeatherReport?> GetUserWeatherReport(string userId, CancellationToken cancel)
        {
            var queryable = container.GetItemLinqQueryable<UserWeatherReport>();

            // Construct LINQ query
            var matches = queryable
                .Where(p => p.Id == userId);

            using FeedIterator<UserWeatherReport> linqFeed = matches.ToFeedIterator();
            if (linqFeed.HasMoreResults)
            {
                FeedResponse<UserWeatherReport> response = await linqFeed.ReadNextAsync(cancel);

                return response.FirstOrDefault();
            }

            return null;
        }

        public async Task<IEnumerable<UserWeatherReport>> GetReportsToUpdate()
        {

            QueryRequestOptions options = new QueryRequestOptions() { MaxBufferedItemCount = 10 };

            var queryable = container.GetItemLinqQueryable<UserWeatherReport>(requestOptions: options);

            var matches = queryable
                .Where(x => x.Updated <= DateTime.UtcNow.AddHours(-12) || !x.Updated.HasValue);

            using FeedIterator<UserWeatherReport> linqFeed = matches.ToFeedIterator();

            if (linqFeed.HasMoreResults)
            {
                FeedResponse<UserWeatherReport> response = await linqFeed.ReadNextAsync();
                return response;
            }
            this.logger.LogTrace("No records found");
            return Enumerable.Empty<UserWeatherReport>();
        }
    }
}
