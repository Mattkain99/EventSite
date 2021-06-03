using EventSiteAPI.Data.Configurations;
using EventSiteAPI.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EventSiteAPI.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<Reveller>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IOptions<OperationalStoreOptions>operationalStoreOptions)
            : base(options, operationalStoreOptions)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .ApplyConfiguration(new CampusConfiguration())
                .ApplyConfiguration(new CityConfiguration())
                .ApplyConfiguration(new EventConfiguration())
                .ApplyConfiguration(new EventRevellerConfiguration())
                .ApplyConfiguration(new PlaceConfiguration())
                .ApplyConfiguration(new RevellerConfiguration());
        }
    }
    
}