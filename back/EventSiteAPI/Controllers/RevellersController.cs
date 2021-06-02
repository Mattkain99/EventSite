using System.Threading.Tasks;
using EventSiteAPI.Data.Repositories;
using EventSiteAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventSiteAPI.Controllers
{
    [ApiController]
    [Route("revellers")]
    public class RevellersController : ControllerBase
    {
        private readonly RevellersRepository _revellersRepository;

        public RevellersController(RevellersRepository revellersRepository)
        {
            _revellersRepository = revellersRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync() => Ok(await _revellersRepository.GetRevellersAsync());

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Reveller reveller)
        {
            await _revellersRepository.AddRevellerAsync(reveller);
            return Ok();
        }
        

    }
}