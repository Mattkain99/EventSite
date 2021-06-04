using System;
using System.Collections.Generic;

namespace EventSiteAPI.Models
{
    public class Place
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public Guid CityId { get; set; }
        public City? City { get; set; }
        public List<Event>? Events { get; set; }
    }
}