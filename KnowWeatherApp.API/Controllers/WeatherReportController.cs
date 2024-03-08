using KnowWeatherApp.API.Interfaces;
using KnowWeatherApp.API.Models;
using KnowWeatherApp.API.Models.OpenWeather;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KnowWeatherApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherReportController : ControllerBase
    {
        private readonly IWeatherReportRepository userWeatherReportRepository;
        private readonly IOpenWeatherRepository openWeatherRepository;
        private readonly ICurrentUserService currentUserService;

        public WeatherReportController(
            IWeatherReportRepository userWeatherReportRepository,
            IOpenWeatherRepository openWeatherRepository,
            ICurrentUserService currentUserService)
        {
            this.userWeatherReportRepository = userWeatherReportRepository;
            this.openWeatherRepository = openWeatherRepository;
            this.currentUserService = currentUserService;
        }

        /// <summary>
        /// Get weather report by a city id
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        [HttpGet("city/{cityId}")]
        [Authorize]
        [ProducesResponseType(typeof(List<CityDto>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Index(string cityId, CancellationToken cancel)
        {
            if(string.IsNullOrWhiteSpace(cityId))
            {
                return BadRequest("City id can not be empty");
            }

            var cityReport = await this.userWeatherReportRepository.GetUserWeatherReport(currentUserService.UserId, cityId, cancel);

            if (cityReport == null)
            {
                return NotFound("City was not found");
            }

            return Ok(cityReport.Adapt<CityDto>());
        }

        /// <summary>
        /// Get weather report by latitude and longitude
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        [HttpGet("coordinates")]
        [ProducesResponseType(typeof(WeatherReportDto), 200)]
        public async Task<IActionResult> GetWeatherReportByCity(double lat, double lon, CancellationToken cancel)
        {
            var cityReport = await this.openWeatherRepository.GetWeatherByLocation(lat, lon, cancel);
            return Ok(cityReport);
        }
    }
}
