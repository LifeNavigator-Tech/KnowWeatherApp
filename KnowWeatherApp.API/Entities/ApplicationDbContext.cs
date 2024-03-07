using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace KnowWeatherApp.API.Entities
{
    public class KnowWeatherDbContext : IdentityDbContext<AppUser>
    {
        public KnowWeatherDbContext(DbContextOptions<KnowWeatherDbContext> options) : base(options)
        {  
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AppUser>()
                .HasMany(e => e.Cities)
                .WithMany(e => e.Users)
                .UsingEntity(
                    l => l.HasOne(typeof(AppUser)).WithMany().OnDelete(DeleteBehavior.NoAction),
                    r => r.HasOne(typeof(City)).WithMany().OnDelete(DeleteBehavior.NoAction));

            base.OnModelCreating(builder);
        }

        public DbSet<City> Cities { get; set; }
    }
}
