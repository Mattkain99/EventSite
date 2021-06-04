using System.Threading.Tasks;
using EventSiteAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace EventSiteAPI.Services
{
    public class IdentityService
    {
        private readonly SignInManager<Reveller> _signInManager;
        private readonly UserManager<Reveller> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityService(UserManager<Reveller> userManager,
            SignInManager<Reveller> signInManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IdentityResult> RegisterAsync(Reveller reveller, string password)
        {
            var result = await _userManager.CreateAsync(reveller, password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(reveller, false);
            }

            return result;
        }

        public async Task<bool> LoginAsync(string userName, string password)
        {
            //var result = await _signInManager.PasswordSignInAsync(userName, password, true, false);
            var user = await _userManager.FindByEmailAsync(userName);
            var checkedPassword = await _userManager.CheckPasswordAsync(user, password);
            if (checkedPassword)
            {
                await _signInManager.SignInAsync(user, false);
                return true;
            }
            return false;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<Reveller> GetPrincipalAsync()
        {
            return await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        }
    }
}