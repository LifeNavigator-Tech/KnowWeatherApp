using KnowWeatherApp.Common.Interfaces;
using KnowWeatherApp.Contracts;
using KnowWeatherApp.Contracts.OpenWeather;
using KnowWeatherApp.Domain.Entities.Weather;
using KnowWeatherApp.Domain.Repositories;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KnowWeatherApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityRepository cityRepository;
        private readonly IOpenWeatherService openWeatherService;
        private readonly ICurrentUserHelper currentUserHelper;

        public CitiesController(
            ICityRepository cityRepository,
            IOpenWeatherService openWeatherService,
            ICurrentUserHelper currentUserHelper)
        {
            this.cityRepository = cityRepository;
            this.openWeatherService = openWeatherService;
            this.currentUserHelper = currentUserHelper;
        }

        /// <summary>
        /// Get weather report by a city id
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        [HttpGet("{cityId}")]
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

            var cityReport = await this.cityRepository.GetWeatherReport(currentUserHelper.UserId, cityId, cancel);

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
        [HttpGet("report")]
        [ProducesResponseType(typeof(WeatherReportDto), 200)]
        public async Task<IActionResult> GetWeatherReportByCity(double lat, double lon, CancellationToken cancel)
        {
            var cityReport = await this.openWeatherService.GetWeatherByLocation(lat, lon, cancel);
            return Ok(cityReport);
        }

        /// <summary>
        /// Search cities by name, state and country
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        [HttpGet("search")]
        [ProducesResponseType(typeof(List<CityDto>), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Search([FromQuery] SearchCityRequestDto request, CancellationToken cancel)
        {
            var result = await this.cityRepository.FindCities(request, cancel);
            if (result == null) return NotFound();
            return Ok(result.AsQueryable().Select(x => x.Adapt<CityDto>()));
        }

        /// <summary>
        /// Get user cities
        /// </summary>
        /// <param name="cancel"></param>
        /// <returns></returns>
        [HttpGet("user")]
        [Authorize]
        [ProducesResponseType(typeof(List<CityDto>), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCitiesByUserId(CancellationToken cancel)
        {
            var result = await this.cityRepository.FindCities(this.currentUserHelper.UserId, cancel);
            if (result == null) return NotFound();
            return Ok(result.AsQueryable().Select(x => x.Adapt<CityDto>()));
        }

        /// <summary>
        /// Add city to user
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        [HttpPost("user")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddCityToUser([FromBody] AddCityToUserRequest request, CancellationToken cancel)
        {
            var city = await cityRepository.AddCityToUser(currentUserHelper.UserId, request.CityId, cancel);

            if (city.WeatherReport == null)
            {
                var cityReport = await this.openWeatherService.GetWeatherByLocation(city.Lat, city.Lon, cancel);
                var report = cityReport.Adapt<WeatherReport>();
                await cityRepository.AssignReportToACityAsync(city.Id, report, cancel);
            }

            return Ok();
        }
    }
}
