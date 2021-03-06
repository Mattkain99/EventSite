using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventSiteAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventSiteAPI.Data.Repositories
{
    public class CampusRepository
    {
        private readonly ApplicationDbContext _context;

        public CampusRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<Campus>> GetCampusAsync() => await _context.Set<Campus>().ToListAsync();

        public async Task AddCampusAsync(Campus campus)
        {
            await _context.AddAsync(campus);
            await _context.SaveChangesAsync();
        }

        

        
        
        // Choix métier : on ajoute pas des reveller ou des events depuis Campus

    }
}