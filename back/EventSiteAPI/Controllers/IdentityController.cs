using System.Threading.Tasks;
using EventSiteAPI.DTO;
using EventSiteAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventSiteAPI.Controllers
{
    [ApiController]
    [Route("api/identity/")]
    public class IdentityController : ControllerBase
    {
        private readonly IdentityService _identityService;

        public IdentityController(IdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDTO register)
        {
            var result = await _identityService.RegisterAsync(register.Reveller, register.Password);
                
            
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors);
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] SignInDTO credentials)
        {
            var current = await _identityService.LoginAsync(credentials.UserName, credentials.Password);
            return Ok(current);
        }

        [HttpGet("logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            await _identityService.LogoutAsync();
            return Ok();
        }

        [HttpGet("current")]
        [Authorize]
        public async Task<IActionResult> GetCurrentAsync()
        {
            return Ok(await _identityService.GetPrincipalAsync());
        }
    }
}