using KnowWeatherApp.API.Entities;
using KnowWeatherApp.API.Interfaces;
using KnowWeatherApp.API.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace KnowWeatherApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserWeatherReportController : ControllerBase
    {
        private readonly IWeatherReportRepository userWeatherReportRepository;
        private readonly IOpenWeatherRepository openWeatherRepository;
        private readonly ICityRepository cityRepository;

        public UserWeatherReportController(
            IWeatherReportRepository userWeatherReportRepository,
            IOpenWeatherRepository openWeatherRepository,
            ICityRepository cityRepository)
        {
            this.userWeatherReportRepository = userWeatherReportRepository;
            this.openWeatherRepository = openWeatherRepository;
            this.cityRepository = cityRepository;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Index(string userId, CancellationToken cancel)
        {
            var reports = await this.userWeatherReportRepository.GetUserWeatherReport(userId, cancel);

            if (!reports.Any())
            {
                return NotFound();
            }
            var reportForCities = reports.AsQueryable().Select(s => s.Adapt<CityDto>());
            return Ok(reportForCities);
        }

        [HttpGet("location")]
        public async Task<IActionResult> GetWeatherReportByCity(double lat, double lon, CancellationToken cancel)
        {
            var cityReport = await this.openWeatherRepository.GetWeatherByLocation(lat, lon, cancel);
            return Ok(cityReport);
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> AddCityToUser(string userId, [FromBody] AddCityToUserRequest request, CancellationToken cancel)
        {
            var result = await cityRepository.AddCityToUser(userId, request.CityId);

            if (result) return Ok();
            return BadRequest();
        }


    }
}
