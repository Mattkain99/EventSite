using System;

namespace EventSiteAPI.Models
{
    public class EventReveller
    {
        public Guid RevellerId { get; set; }
        public Reveller Reveller { get; set; }

        public Guid EventId { get; set; }
        public Event Event { get; set; }
    }
}