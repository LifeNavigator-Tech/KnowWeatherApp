﻿using KnowWeatherApp.Common.Interfaces;
using KnowWeatherApp.Contracts;
using KnowWeatherApp.Domain.Repositories;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KnowWeatherApp.API.Controllers
{
    /// <summary>
    /// Represents cities resource.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityRepository cityRepository;
        private readonly ICurrentUserHelper currentUserHelper;

        /// <summary>
        /// Constructor with dependencies.
        /// </summary>
        /// <param name="cityRepository"></param>
        /// <param name="currentUserHelper"></param>
        public CitiesController(
            ICityRepository cityRepository,
            ICurrentUserHelper currentUserHelper)
        {
            this.cityRepository = cityRepository;
            this.currentUserHelper = currentUserHelper;
        }
        /// <summary>
        /// Search cities by name, state and country
        /// </summary>
        /// <param name="city"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        [HttpGet("search")]
        [ProducesResponseType(typeof(List<CityDto>), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Search([FromQuery] SearchCityRequestDto request, CancellationToken cancel)
        {
            var result = await this.cityRepository.FindByCityAsync(request.Name, request.State, request.Country, cancel);
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
            var result = await this.cityRepository.FindCitiesByUserId(this.currentUserHelper.UserId, cancel);
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
            var result = await cityRepository.AddCityToUser(currentUserHelper.UserId, request.CityId, cancel);

            if (result) return Ok();
            return BadRequest();
        }
    }
}
