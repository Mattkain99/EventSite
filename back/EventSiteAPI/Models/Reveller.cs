using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace EventSiteAPI.Models
{
    public class Reveller : IdentityUser
    {
        // Email, phone, password sont dans IdentityUser
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public Guid CampusId { get; set; }
        public Campus Campus { get; set; }
        public List<Event> Events { get; set; }
        
        public List<EventReveller> EventRevellers { get; set; }
        
    }
}