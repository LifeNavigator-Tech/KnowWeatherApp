using KnowWeatherApp.API.Interfaces;
using KnowWeatherApp.API.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace KnowWeatherApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityRepository cityRepository;

        public CitiesController(ICityRepository cityRepository)
        {
            this.cityRepository = cityRepository;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] SearchCityRequestDto city)
        {
            var result = await this.cityRepository.FindByCityAsync(city);
            if (result == null) return NotFound();
            return Ok(result.AsQueryable().ProjectToType<CityDto>());
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCitiesByUserId(string userId)
        {
            var result = await this.cityRepository.FindCitiesByUserId(userId);
            if (result == null) return NotFound();
            return Ok(result.AsQueryable().ProjectToType<CityDto>());
        }
    }
}
