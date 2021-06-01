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
        
        public async Task<IReadOnlyCollection<Event>> GetEventsAsync(EventFilter filter)
        {
            
            var principal = new Reveller();
            if (filter.IncludeSubscribedEvent)
            {
                IncludeSubscribedEventQuery(query, principal);
            }

            if (filter.IncludeNotSubscribedEvent)
            {
            }        
            
        }

        private async Task<IReadOnlyCollection<Event>> IncludeNotSubsribedQuery(Reveller principal, EventFilter filter)
        {
            var query = _context.Set<Event>().AsQueryable().Where(BuildQuery(filter));

            var result =  await query.GroupJoin(_context.Set<EventReveller>(), e => e.Id, er => er.EventId,
                    (e, er) => new {e, er})
                .SelectMany(x => x.er.DefaultIfEmpty(), (e, er) => new {e, er})
                .ToListAsync();
                return result.Where(x => x.er == null || x.er.RevellerId != principal.Id)
                .Select(x => x.e).ToList();
        }

        private void IncludeSubscribedEventQuery(IQueryable<Event> query, Reveller principal)
        {
            query.GroupJoin(_context.Set<EventReveller>(), e => e.Id, er => er.EventId,
                    (e, er) => new {e, er})
                .SelectMany(x => x.er.DefaultIfEmpty(), (e, er) => new {e, er})
                .Where(x => x.er != null && x.er.RevellerId == principal.Id);
        }

        public async Task AddEventAsync(Event entity, Guid creatorId)
        {
            await _context.AddAsync(entity);
            await _context.AddAsync(new EventReveller
            {
                EventId = entity.Id, RevellerId = creatorId
            });
            await _context.SaveChangesAsync();
        }
        public async Task<IReadOnlyCollection<Event>> GetCampusEventAsync(Guid campusId) =>
            await _context.Set<Event>().Where(e => e.CampusId == campusId).ToListAsync();

        private Expression<Func<Event, bool>> BuildQuery(EventFilter filter)
        {
            Expression<Func<Event, bool>> expression = e => e.Id != null;
            
            var principal = new Reveller();         //temporaire pour gérer identity
            
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

            if (filter.IncludeCreator)
            {
                expression.AndAlso(e => e.CreatorId == principal.Id);
            }

            if (filter.IncludePastEvent)
            {
                expression.AndAlso(e => e.BeginTime <= DateTime.Today);
            }

            return expression;
        }
    }
}