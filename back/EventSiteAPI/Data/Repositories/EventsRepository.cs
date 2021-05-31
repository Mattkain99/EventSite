using System.Collections.Generic;
using System.Threading.Tasks;
using EventSiteAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventSiteAPI.Data.Repositories
{
    public class EventsRepository
    {
        private readonly ApplicationDbContext _context;


        public EventsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IReadOnlyCollection<Event>> GetEventsAsync() => await _context.Set<Event>().ToListAsync();
        
        public async Task AddEventAsync(Event events)
        {
            await _context.AddAsync(events);
            await _context.SaveChangesAsync();
        }
    }
}