using KnowWeatherApp.API.Entities.Weather;
using System.ComponentModel.DataAnnotations;

namespace KnowWeatherApp.API.Entities;

public class City
{
    [Key]
    public string Id { get; set; }
    public List<AppUser> Users { get; set; } = new List<AppUser>();
    public virtual WeatherReport? WeatherReport { get; set; }
    [StringLength(256)]
    public string Name { get; set; }
    [StringLength(256)]
    public string State { get; set; }
    [StringLength(256)]
    public string Country { get; set; }
    public double Lat { get; set; }
    public double Lon { get; set; }
}