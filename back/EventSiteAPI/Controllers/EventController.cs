using System;
using System.Threading.Tasks;
using EventSiteAPI.Data.Repositories;
using EventSiteAPI.Models;
using EventSiteAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventSiteAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/events")]
    public class EventController : ControllerBase
    {
        private readonly EventsService _eventsService;

        public EventController(EventsService eventsService)
        {
            _eventsService = eventsService;
        }

        [HttpPost("filteredEvents")]
        public async Task<IActionResult> GetAsync([FromBody] EventFilter filter) => Ok(await _eventsService.GetFilteredEventsAsync(filter));

        [HttpPost]
        public async Task<IActionResult> AddEventAsync([FromBody] Event upcomingEvent)
        {
            await _eventsService.AddEventAsync(upcomingEvent);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEventAsync([FromBody] Event modifiedEvent)
        {
            await _eventsService.UpdateEventAsync(modifiedEvent);
            return Ok();
        }

        [HttpDelete("{eventId}")]
        public async Task<IActionResult> DeleteEventAsync(Guid eventId)
        {
            await _eventsService.DeleteEventAsync(eventId);
            return Ok();
        }

        [HttpPost("{eventId}/unsubscribe")]
        public async Task<IActionResult> UnsubscribeEventAsync(Guid eventId)
        {
            try
            {
                await _eventsService.UnsubscribeEventAsync(eventId);
                return Ok();
            }
            catch (InvalidOperationException exception)
            {
                return BadRequest();
            }
        }
        
        
        [HttpPost("{eventId}/subscribe")]
        public async Task<IActionResult> SubscribeAsync(Guid eventId)
        {
            try
            {
                await _eventsService.SubscribeEventAsync(eventId);
                return Ok();
            }
            catch (ArgumentException exception)
            {
                return NotFound();
            }
            catch (InvalidOperationException exception)
            {
                return BadRequest();
            }
        }
    }
}