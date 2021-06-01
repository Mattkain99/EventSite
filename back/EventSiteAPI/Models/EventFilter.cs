using System;

namespace EventSiteAPI.Models
{
    public class EventFilter
    {
        public Guid? CampusId { get; set; }
        public string? EventName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IncludeCreator { get; set; }
        public bool IncludeSubscribedEvent { get; set; }
        public bool IncludeNotSubscribedEvent { get; set; }
        public bool IncludePastEvent { get; set; }
        
    }
}