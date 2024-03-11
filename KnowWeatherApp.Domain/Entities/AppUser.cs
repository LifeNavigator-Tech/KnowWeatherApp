using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace KnowWeatherApp.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public List<City> Cities { get; set; } = new List<City>();
        public List<Trigger> Triggers { get; set; } = new List<Trigger>();
        [StringLength(256)]
        public string FirstName { get; set; }
        [StringLength(256)]
        public string LastName { get; set; }
        public int TimeZoneOffset { get; set; }
    }
}
