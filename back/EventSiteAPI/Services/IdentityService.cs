using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using EventSiteAPI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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

        public async Task<SignInResult> LoginAsync(string userName, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(userName, password, true, false);
            return result;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<Reveller> GetPrincipalAsync()
        {
            return await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        }
    }
}