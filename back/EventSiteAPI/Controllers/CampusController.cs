﻿using System.Threading.Tasks;
using EventSiteAPI.Data.Repositories;
using EventSiteAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventSiteAPI.Controllers
{
    [ApiController]
    [Route("campus")]
    public class CampusController : ControllerBase
    {
        private readonly CampusRepository _campusRepository;

        public CampusController(CampusRepository campusRepository)
        {
            _campusRepository = campusRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync() => Ok(await _campusRepository.GetCampusAsync());

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Campus campus)
        {
            await _campusRepository.AddCampusAsync(campus);
            return Ok();
        }

    }
}