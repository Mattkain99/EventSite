using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EventSiteAPI.Extensions;
using EventSiteAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EventSiteAPI.Data.Repositories
{
    public class EventsRepository
    {
        private readonly ApplicationDbContext _context;


        public EventsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Event> GetEventAsync(Guid eventId) =>
            await _context.Set<Event>()
                .Include(e=>e.EventRevellers)
                .FirstOrDefaultAsync(e => e.Id == eventId);
            
        public async Task<IReadOnlyCollection<Event>> GetEventsAsync(EventFilter filter)
        {
            // On génére le filtre une seule fois
            var expressionFilter = BuildQueryFilter(filter);
            // On crée une query de base que l'on va agrémenter selon les bool includeCreator / includeSubscribed / includeNotSubscribed
            var baseQuery = _context.Set<Event>().Include(e => e.Creator).Where(expressionFilter);
            var principal = new Reveller(); // Temporaire le temps de gérer le principal avec Identity

            if (filter.IncludeCreator)
            {
                // On filtre sur la query de base directement
                baseQuery.Where(e => e.CreatorId == principal.Id);
            }

            // Si on veut les event souscrit et non souscrit => on veut l'ensemble des events donc pas besoin d'appliquer les union
            if (filter.IncludeSubscribedEvent && filter.IncludeNotSubscribedEvent)
            {
                return await baseQuery.ToListAsync();
            }

            if (filter.IncludeSubscribedEvent)
            {
                var includeSubscribedEventQuery = IncludeSubscribedEventQuery(principal, expressionFilter);
                // Eq. SELECT * FROM Event UNION nouvelle query
                baseQuery.Union(includeSubscribedEventQuery);
            }

            if (filter.IncludeNotSubscribedEvent)
            {
                var includeNotSubscribedEventQuery = IncludeNotSubsribedQuery(principal, expressionFilter);
                baseQuery.Union(includeNotSubscribedEventQuery);
            }

            return await baseQuery.ToListAsync();
        }

        private IQueryable<Event> IncludeSubscribedEventQuery(Reveller principal, Expression<Func<Event, bool>> expressionFilter) =>
            // Eq. SELECT * FROM Event INNER JOIN EventReveller ON Event.Id = ER.EventId AND ER.RevellerId = 4; => ou 4 est le principal.Id càd la personne connectée
            _context.Set<Event>()
                .Include(e=>e.Creator)
                .Where(expressionFilter)
                .Join(
                    _context.Set<EventReveller>(),
                    e => new { EventId = e.Id, RevellerId = principal.Id },
                    er => new { er.EventId, er.RevellerId },
                    (e, _) => e
                );

        private IQueryable<Event> IncludeNotSubsribedQuery(Reveller principal, Expression<Func<Event, bool>> expressionFilter) =>
            // Eq. SELECT * FROM Event LEFT OUTER JOIN ER ON Event.Id = ER.EventId WHERE ER.RevellerId != principal.Id
            _context.Set<Event>()
                .Include(e=>e.Creator)
                .Where(expressionFilter)
                .GroupJoin(
                    _context.Set<EventReveller>(),
                    e => e.Id,
                    er => er.EventId,
                    (e, er) => new {e, er})
                .SelectMany(
                    x => x.er.DefaultIfEmpty(),
                    (x, er) => new { x.e, er })
                .Where(x => x.er == null || x.er.RevellerId != principal.Id)
                .Select(x => x.e);

        public async Task AddEventAsync(Event entity, Guid creatorId)
        {
            await _context.AddAsync(entity);
            await _context.AddAsync(new EventReveller
            {
                EventId = entity.Id, RevellerId = creatorId
            });
            await _context.SaveChangesAsync();
            
        }

        public async Task UpdateAsync(Event entity)
        {
            _context.Entry(GetEventAsync(entity.Id)).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddEventRevellerAsync(Guid eventId, Guid revellerId)
        {
            await _context.AddAsync(new EventReveller
            {
                EventId = eventId, RevellerId = revellerId
            });
            await _context.SaveChangesAsync();
        }
        public async Task<IReadOnlyCollection<Event>> GetCampusEventAsync(Guid campusId) =>
            await _context.Set<Event>().Where(e => e.CampusId == campusId).ToListAsync();

        private Expression<Func<Event, bool>> BuildQueryFilter(EventFilter filter)
        {
            Expression<Func<Event, bool>> expression = e => true;

            if (filter.CampusId != null)
            {
                expression.AndAlso(e => e.CampusId == filter.CampusId);
            }

            if (!string.IsNullOrEmpty(filter.EventName))
            {
                expression.AndAlso(e => e.Name.StartsWith(filter.EventName));
            }

            if (filter.StartDate != null)
            {
                expression.AndAlso(e => filter.StartDate.Value <= e.BeginTime);
            }

            if (filter.EndDate != null)
            {
                expression.AndAlso(e => filter.EndDate.Value >= e.BeginTime);
            }

            if (filter.IncludePastEvent)
            {
                expression.AndAlso(e => e.BeginTime <= DateTime.Today);
            }

            return expression;
        }

        public async Task DeleteEventAsync(Event toDeleteEvent)
        {
            _context.Remove(toDeleteEvent);
            await _context.SaveChangesAsync();
        }

        public async Task<EventReveller> GetEventRevellerAsync(Guid eventId, Guid revellerId) =>
            await _context.Set<EventReveller>()
                .Include(er=>er.Event)
                .ThenInclude(e=>e.EventRevellers)
                .FirstOrDefaultAsync(er => er.RevellerId == revellerId && er.EventId == eventId);

        public async Task DeleteEventRevellerAsync(EventReveller eventRevellerToDelete)
        {
            _context.Remove(eventRevellerToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<Event> GetEventToDeleteAsync(Guid eventId) =>
            await _context.Set<Event>().FirstOrDefaultAsync(e => e.Id == eventId);
    }
}