using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventSiteAPI.Data.Repositories;
using EventSiteAPI.DTO;
using EventSiteAPI.Models;

namespace EventSiteAPI.Services
{
    public class EventsService
    {
        private readonly EventsRepository _eventsRepository;

        public EventsService(EventsRepository eventsRepository)
        {
            _eventsRepository = eventsRepository;
        }

        public async Task SubscribeEventAsync(Guid eventId)
        {
            var selectedEvent = await _eventsRepository.GetEventAsync(eventId);
            if (selectedEvent == null)
            {
                throw new ArgumentException();
            }

            if (selectedEvent.Status != Status.Open || selectedEvent.IsFull)
            {
                throw new InvalidOperationException();
            }

            await _eventsRepository.AddEventRevellerAsync(eventId, new Reveller().Id);
            if (selectedEvent.IsFull || selectedEvent.SubscribeDeadline > DateTime.Today)
            {
                selectedEvent.Status = Status.Closed;
                await UpdateEventAsync(selectedEvent);
            }
        }

        public async Task AddEventAsync(Event upcomingEvent)
        {
            upcomingEvent.Status = Status.Draft;
            await _eventsRepository.AddEventAsync(upcomingEvent, new Reveller().Id);
        }

        public async Task UpdateEventAsync(Event modifiedEvent)
        {
            await _eventsRepository.UpdateAsync(modifiedEvent);
        }

        public async Task DeleteEventAsync(Guid eventId)
        {
            var toDeleteEvent = await _eventsRepository.GetEventToDeleteAsync(eventId);
            await _eventsRepository.DeleteEventAsync(toDeleteEvent);
        }

        public async Task UnsubscribeEventAsync(Guid eventId)
        {
            var eventRevellerToDelete = await _eventsRepository.GetEventRevellerAsync(eventId, new Reveller().Id);
            var currentEvent = eventRevellerToDelete.Event;
            if (eventRevellerToDelete == null || currentEvent.Status != Status.Open ||
                (currentEvent.Status == Status.Closed && currentEvent.SubscribeDeadline > DateTime.Today))
            {
                throw new InvalidOperationException();
            }

            await _eventsRepository.DeleteEventRevellerAsync(eventRevellerToDelete);
            if (currentEvent.EventRevellers.Count == 0) // A verifier que ca fonctionne ( sinon mettre un -1 )
            {
                await _eventsRepository.DeleteEventAsync(currentEvent);
            }

            if (currentEvent.EventRevellers.Count < currentEvent.MaxMembers)
            {
                currentEvent.Status = Status.Open;
                await UpdateEventAsync(currentEvent);
            }

            
        }

        public async Task<List<EventDTO>> GetFilteredEventsAsync(EventFilter filter)
        {
            var filteredEvents = await _eventsRepository.GetEventsAsync(filter);
            return filteredEvents.Select(e => EventDTO.FromEvent(e, new Reveller().Id)).ToList();
        }
    }
}