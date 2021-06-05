using System;
using System.Collections.Generic;

namespace EventSiteAPI.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime BeginTime { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime SubscribeDeadline { get; set; }
        public bool IsFull => (EventRevellers?.Count ?? 0) == MaxMembers;
        
        public int MaxMembers { get; set; }
        public string Infos { get; set; }
        public Status Status { get; set; }
        public Guid PlaceId { get; set; }
        public Place? Place { get; set; }
        public Guid CampusId { get; set; }
        public Campus? Campus { get; set; }
        public string CreatorId { get; set; }
        public Reveller? Creator { get; set; }
        public List<EventReveller>? EventRevellers { get; set; }
        
    }
}