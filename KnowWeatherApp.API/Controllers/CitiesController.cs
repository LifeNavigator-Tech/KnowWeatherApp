using KnowWeatherApp.Common.Interfaces;
using KnowWeatherApp.Contracts;
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
        private readonly IWeatherReportRepository weatherReportRepository;

        public CitiesController(
            ICityRepository cityRepository,
            IOpenWeatherService openWeatherService,
            ICurrentUserHelper currentUserHelper,
            IWeatherReportRepository weatherReportRepository)
        {
            this.cityRepository = cityRepository;
            this.openWeatherService = openWeatherService;
            this.currentUserHelper = currentUserHelper;
            this.weatherReportRepository = weatherReportRepository;
        }

        /// <summary>
        /// Search cities by name, state and country
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        [HttpGet]
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
                var cityRequest = new GetWeatherReportByLocationRequest(city.Lat, city.Lon);
                var cityReport = await this.openWeatherService.GetWeatherByLocation(cityRequest, cancel);
                var report = cityReport.Adapt<WeatherReport>();
                await weatherReportRepository.AssignReportToACityAsync(city.Id, report, cancel);
            }

            return Ok();
        }
    }
}
