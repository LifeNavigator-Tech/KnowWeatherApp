using KnowWeatherApp.API.Interfaces;
using KnowWeatherApp.API.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace KnowWeatherApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressesController : ControllerBase
    {
        private readonly ICityCosmoRepository cityRepository;

        public AddressesController(ICityCosmoRepository cityRepository)
        {
            this.cityRepository = cityRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] RequestCityDto city)
        {
            var result = await this.cityRepository.FindByCityAsync(city);
            if (result == null) return NotFound();
            return Ok(result.AsQueryable().ProjectToType<CityDto>());
        }
    }
}
