using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventSiteAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventSiteAPI.Data.Repositories
{
    public class CitiesRepository
    {
        private readonly ApplicationDbContext _context;

        public CitiesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<City>> GetCitiesAsync() => await _context.Set<City>().ToListAsync();         //equivalent a SELECT* From City

        public async Task AddCityAsync(City city)                   // equivalent a insert into city 
        {
            await _context.AddAsync(city);
            await _context.SaveChangesAsync();          
        }

        public async Task<IReadOnlyCollection<Place>> GetCityPlacesAsync(Guid cityId) =>
            await _context.Set<Place>().Where(p => p.CityId == cityId).ToListAsync();       //SELECT * From place WHERE Place.CityId = CityId

        public async Task AddPlaceAsync(Place place)
        {
            await _context.AddAsync(place);
            await _context.SaveChangesAsync();
        }
    }
}