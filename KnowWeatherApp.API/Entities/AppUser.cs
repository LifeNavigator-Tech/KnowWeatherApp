using Microsoft.AspNetCore.Identity;

namespace KnowWeatherApp.API.Entities
{
    public class AppUser : IdentityUser
    {
        public ICollection<City> Cities { get; set; }
    }
}
