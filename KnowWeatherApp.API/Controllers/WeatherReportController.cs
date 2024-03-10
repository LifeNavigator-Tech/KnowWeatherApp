using KnowWeatherApp.Contracts.OpenWeather;
using KnowWeatherApp.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KnowWeatherApp.Domain.Repositories;
using KnowWeatherApp.Common.Interfaces;
using Mapster;

namespace KnowWeatherApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherReportController : ControllerBase
    {
        private readonly IWeatherReportRepository weatherReportRepository;
        private readonly ICurrentUserHelper currentUserHelper;
        private readonly ICityRepository cityRepository;
        private readonly IOpenWeatherService openWeatherService;

        public WeatherReportController(
            IWeatherReportRepository weatherReportRepository,
            ICurrentUserHelper currentUserHelper,
            ICityRepository cityRepository,
            IOpenWeatherService openWeatherService)
        {
            this.weatherReportRepository = weatherReportRepository;
            this.currentUserHelper = currentUserHelper;
            this.cityRepository = cityRepository;
            this.openWeatherService = openWeatherService;
        }

        /// <summary>
        /// Get weather report by city
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        [HttpGet("{cityId}")]
        [Authorize]
        [ProducesResponseType(typeof(List<CityDto>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetWeatherReportByCityId(GetWeatherReportByCityRequest request, CancellationToken cancel)
        {
            var cityExists = await this.cityRepository.ExistsAsync(request.CityId, cancel);

            if (!cityExists)
            {
                return BadRequest("City with provided cityId doesn't exist");
            }

            var report = await this.weatherReportRepository.GetWeatherReport(currentUserHelper.UserId, request.CityId, cancel);

            return Ok(report.Adapt<WeatherReportDto>());
        }

        /// <summary>
        /// Get weather report by location
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(WeatherReportDto), 200)]
        public async Task<IActionResult> GetWeatherReportByLocation([FromQuery]GetWeatherReportByLocationRequest request, CancellationToken cancel)
        {
            var cityReport = await this.openWeatherService.GetWeatherByLocation(request, cancel);
            return Ok(cityReport);
        }
    }
}
