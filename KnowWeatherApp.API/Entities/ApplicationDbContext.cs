﻿using KnowWeatherApp.API.Entities.Weather;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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

            builder.Entity<City>()
                .HasOne(x => x.WeatherReport)
                .WithOne(x => x.City);

            builder.Entity<WeatherReport>()
                .OwnsMany(
                    user => user.HourlyReports, ownedNavigationBuilder =>
                    {
                        ownedNavigationBuilder.ToJson();
                    })
                .OwnsMany(user => user.DailyReports, ownedNavigationBuilder =>
                {
                    ownedNavigationBuilder.ToJson();
                    ownedNavigationBuilder.OwnsOne(x => x.Temp);
                    ownedNavigationBuilder.OwnsOne(x => x.FeelsLike);
                })
                .OwnsOne(user => user.Current, ownedNavigationBuilder =>
                {
                    ownedNavigationBuilder.ToJson();
                });

            base.OnModelCreating(builder);
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<WeatherReport> WeatherReports { get; set; }
    }
}