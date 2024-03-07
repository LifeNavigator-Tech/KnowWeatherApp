using System.ComponentModel.DataAnnotations;

namespace KnowWeatherApp.API.Entities;

public class City
{
    public int Id { get; set; }
    public ICollection<AppUser> Users { get; set; }
    [StringLength(256)]
    public string Name { get; set; }
    [StringLength(256)]
    public string State { get; set; }
    [StringLength(256)]
    public string Country { get; set; }
    public double Lat { get; set; }
    public double Lon { get; set; }
}