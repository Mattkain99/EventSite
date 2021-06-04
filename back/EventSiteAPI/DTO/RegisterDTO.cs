using EventSiteAPI.Models;

namespace EventSiteAPI.DTO
{
    public class RegisterDTO
    {
        public Reveller Reveller { get; set; }
        public string Password { get; set; }
    }
}