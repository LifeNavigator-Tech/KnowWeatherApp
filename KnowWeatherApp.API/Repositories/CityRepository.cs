﻿using KnowWeatherApp.API.Entities;
using KnowWeatherApp.API.Interfaces;
using KnowWeatherApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace KnowWeatherApp.API.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly KnowWeatherDbContext dbContext;

        public CityRepository(KnowWeatherDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<City>> FindByCityAsync(SearchCityRequestDto cityRequest)
            => await this.dbContext.Cities.Where(x => x.Name.StartsWith(cityRequest.Name)
                        && (!string.IsNullOrEmpty(cityRequest.State) ? x.State.Equals(cityRequest.State) : true)
                        && (!string.IsNullOrEmpty(cityRequest.Country) ? x.Country.Equals(cityRequest.Country) : true))
                         .ToListAsync();

        public async Task<IEnumerable<City>> FindCitiesByUserId(string userId)
            => await this.dbContext.Cities.Where(x => x.Users.Select(u => u.Id).Contains(userId))
                         .ToListAsync();

        public async Task<bool> AddCityToUser(string userId, string cityId)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.Id == userId);
            var city = dbContext.Cities.FirstOrDefault(x => x.Id == cityId);

            if (user != null && city != null)
            {
                user.Cities.Add(city);
                await dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
