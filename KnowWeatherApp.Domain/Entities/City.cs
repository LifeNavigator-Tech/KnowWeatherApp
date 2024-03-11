using System.ComponentModel.DataAnnotations;
using KnowWeatherApp.Domain.Entities.Weather;

namespace KnowWeatherApp.Domain.Entities;

public class City
{
    [Key]
    public string Id { get; set; }
    public List<AppUser> Users { get; set; } = new List<AppUser>();
    public List<Trigger> Triggers { get; set; } = new List<Trigger>();
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