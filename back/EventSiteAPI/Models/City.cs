using System;
using System.Collections.Generic;

namespace EventSiteAPI.Models
{
    public class City
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ZipCode { get; set; }
        public List<Place>? Places { get; set; }
    }
}