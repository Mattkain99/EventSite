using System;
using System.Collections.Generic;

namespace EventSiteAPI.Models
{
    public class Campus
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Event> Events { get; set; }
        public List<Reveller> Revellers { get; set; }
    }
}