using KnowWeatherApp.API.Entities;
using KnowWeatherApp.API.Interfaces;
using KnowWeatherApp.API.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using System.Text.Json;
using MongoDatabaseSettings = KnowWeatherApp.API.Configurations.MongoDatabaseSettings;

namespace KnowWeatherApp.API.Repositories
{
    public class CityCosmoRepository : ICityCosmoRepository
    {
        private Container container;
        public CityCosmoRepository(IOptions<MongoDatabaseSettings> databaseSettings)
        {
            var cosmoClient = new CosmosClient(databaseSettings.Value.ConnectionString, new CosmosClientOptions()
            {
                SerializerOptions = new CosmosSerializationOptions()
                {
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase,
                }
            });
            this.container = cosmoClient.GetDatabase(databaseSettings.Value.DatabaseName).GetContainer("cities");
        }

        public async Task<IEnumerable<City>> FindByCityAsync(RequestCityDto cityRequest)
        {
            IOrderedQueryable<City> queryable = container.GetItemLinqQueryable<City>();

            // Construct LINQ query
            var matches = queryable
                .Where(p => p.Name == cityRequest.Name 
                && (!string.IsNullOrEmpty(cityRequest.State) ? p.State == cityRequest.State : true)
                && (!string.IsNullOrEmpty(cityRequest.Country) ? p.Country == cityRequest.Country : true));

            using FeedIterator<City> linqFeed = matches.ToFeedIterator();
            if (linqFeed.HasMoreResults)
            {
                FeedResponse<City> response = await linqFeed.ReadNextAsync();

                return response;
            }

            return Enumerable.Empty<City>();
        }
    }
}
