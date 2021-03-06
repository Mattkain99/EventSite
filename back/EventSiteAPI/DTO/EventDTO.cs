using System;
using System.Linq;
using System.Text.Json.Serialization;
using EventSiteAPI.Models;

namespace EventSiteAPI.DTO
{
    public class EventDTO
    {
        public Guid EventId { get; set; }
    
        public string Name { get; set; }

        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public int NbSubscribers { get; set; }
        public int MaxSubscribers { get; set; }
        public Status Status { get; set; }

        public bool IsSubscribed { get; set; }
        public string CreatorId { get; set; }
        public string CreatorName { get; set; }

        public static EventDTO FromEvent(Event eventToDTO, string principalId)
        {
            return new EventDTO
            {
                EventId = eventToDTO.Id,
                Name = eventToDTO.Name,
                BeginTime = eventToDTO.BeginTime,
                EndTime = eventToDTO.SubscribeDeadline,
                NbSubscribers = eventToDTO.EventRevellers?.Count ?? 0,
                MaxSubscribers = eventToDTO.MaxMembers,
                Status = eventToDTO.Status,
                IsSubscribed = eventToDTO.EventRevellers?.FirstOrDefault(e => e.RevellerId == principalId) != null,
                CreatorId = eventToDTO.CreatorId,
                CreatorName = $"{eventToDTO.Creator?.FirstName} {eventToDTO.Creator?.LastName}"
            };
            
            
        }
    }
}