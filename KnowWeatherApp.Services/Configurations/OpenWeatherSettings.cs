namespace KnowWeatherApp.Services.Configurations;

public class OpenWeatherSettings
{
    public required string API { get; set; }
    public required string APIKEY { get; set; }
    public required int UpdateIntervalInMinutes { get; set; }
}