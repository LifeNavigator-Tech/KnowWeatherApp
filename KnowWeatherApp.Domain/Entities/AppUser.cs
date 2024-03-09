using Microsoft.AspNetCore.Identity;

namespace KnowWeatherApp.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public List<City> Cities { get; set; } = new List<City>();
    }
}
