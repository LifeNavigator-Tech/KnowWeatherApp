using KnowWeatherApp.API.Entities;
using KnowWeatherApp.API.Interfaces;
using KnowWeatherApp.API.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace KnowWeatherApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserWeatherReportController : ControllerBase
    {
        private readonly IUserWeatherReportRepository userWeatherReportRepository;
        private readonly IOpenWeatherRepository openWeatherRepository;

        public UserWeatherReportController(
            IUserWeatherReportRepository userWeatherReportRepository,
            IOpenWeatherRepository openWeatherRepository)
        {
            this.userWeatherReportRepository = userWeatherReportRepository;
            this.openWeatherRepository = openWeatherRepository;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Index(string userId, CancellationToken cancel)
        {
            var report = await this.userWeatherReportRepository.GetUserWeatherReport(userId, cancel);

            if (report == null)
            {
                return NotFound();
            }

            return Ok(report.Adapt<UserWeatherReportDto>());
        }

        [HttpGet("location")]
        public async Task<IActionResult> GetWeatherReportByCity(double lat, double lon, CancellationToken cancel)
        {
            var cityReport = await this.openWeatherRepository.GetWeatherByCity(new Location() { Lat = lat, Lon = lon }, cancel);
            return Ok(cityReport);
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> CreateOrUpdateUserWeatherReport(string userId, [FromBody] CreateUserWeatherReportDto report, CancellationToken cancel)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest();
            }
            var userReport = await this.userWeatherReportRepository.GetUserWeatherReport(userId, cancel);

            if (userReport != null)
            {
                userReport.Cities = report.Cities.Select(x => x.Adapt<City>()).ToList();
                await this.userWeatherReportRepository.CreateOrUpdateUserWeatherReport(userId, userReport, cancel);
            }


            return Ok();
        }


    }
}
