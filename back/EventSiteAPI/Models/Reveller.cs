using System;
using System.Collections.Generic;

namespace EventSiteAPI.Models
{
    public class Reveller
    {
        public Guid Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public Guid CampusId { get; set; }
        public Campus Campus { get; set; }
        public List<Event> Events { get; set; }
        public List<EventReveller> EventRevellers { get; set; }
        
    }
}