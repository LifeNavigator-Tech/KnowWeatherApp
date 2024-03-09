using KnowWeatherApp.Common.Interfaces;
using KnowWeatherApp.Contracts;
using KnowWeatherApp.Contracts.OpenWeather;
using KnowWeatherApp.Domain.Repositories;
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
        private readonly IOpenWeatherService openWeatherService;
        private readonly ICurrentUserHelper currentUserHelper;

        public WeatherReportController(
            IWeatherReportRepository userWeatherReportRepository,
            IOpenWeatherService openWeatherRepository,
            ICurrentUserHelper currentUserHelper)
        {
            this.userWeatherReportRepository = userWeatherReportRepository;
            this.openWeatherService = openWeatherService;
            this.currentUserHelper = currentUserHelper;
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
            if (string.IsNullOrWhiteSpace(cityId))
            {
                return BadRequest("City id can not be empty");
            }

            var cityReport = await this.userWeatherReportRepository.GetUserWeatherReport(currentUserHelper.UserId, cityId, cancel);

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
            var cityReport = await this.openWeatherService.GetWeatherByLocation(lat, lon, cancel);
            return Ok(cityReport);
        }
    }
}
