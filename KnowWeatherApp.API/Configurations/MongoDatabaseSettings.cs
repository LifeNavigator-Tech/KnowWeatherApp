namespace KnowWeatherApp.API.Configurations;

public class MongoDatabaseSettings
{
    public required string ConnectionString { get; set; }

    public required string DatabaseName { get; set; }

    public required string CollectionName { get; set; }
}
