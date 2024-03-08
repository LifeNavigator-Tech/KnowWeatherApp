using KnowWeatherApp.API.Entities.Weather;
using Microsoft.AspNetCore.Identity;

namespace KnowWeatherApp.API.Entities
{
    public class AppUser : IdentityUser
    {
        public List<City> Cities { get; set; } = new List<City>();
    }
}
