using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventSiteAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventSiteAPI.Data.Repositories
{
    public class RevellersRepository
    {
        private readonly ApplicationDbContext _context;

        public RevellersRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IReadOnlyCollection<Reveller>> GetRevellersAsync() => await _context.Set<Reveller>().ToListAsync();
        
        public async Task AddRevellerAsync(Reveller reveller)
        {
            await _context.AddAsync(reveller);
            await _context.SaveChangesAsync();
        }
        
        public async Task<IReadOnlyCollection<Reveller>> GetCampusRevellerAsync(Guid campusId) =>
            await _context.Set<Reveller>().Where(r => r.CampusId == campusId).ToListAsync();
       
        /* public async Task<List<EventReveller>> GetRevellerEventAsync(Guid eventId) =>
            await _context.Set<EventReveller>().Where(er => er.EventId == eventId).ToListAsync();       // A verifier, requete vers la table d'association

        public async Task<IReadOnlyCollection<Event>> GetCreatorEventsAsync(Guid eventId) =>
            await _context.Set<Event>().Where(e => e.CreatorId == eventId).ToListAsync(); //tentative de bypass de la table d'association */




    }
}