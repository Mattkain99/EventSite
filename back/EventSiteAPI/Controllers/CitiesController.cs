using System;
using System.Threading.Tasks;
using EventSiteAPI.Data.Repositories;
using EventSiteAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventSiteAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly CitiesRepository _citiesRepository;

        public CitiesController(CitiesRepository citiesRepository)
        {
            _citiesRepository = citiesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync() => Ok(await _citiesRepository.GetCitiesAsync());

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] City city)
        {
            await _citiesRepository.AddCityAsync(city);
            return Ok();
        }

        [HttpGet("{cityId}/places")]
        public async Task<IActionResult> GetPlacesAsync(Guid cityId) => Ok(await _citiesRepository.GetCityPlacesAsync(cityId));


    }
}