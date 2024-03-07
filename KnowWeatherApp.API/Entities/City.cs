namespace KnowWeatherApp.API.Entities;

public class City
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public Location Coord { get; set; }
}