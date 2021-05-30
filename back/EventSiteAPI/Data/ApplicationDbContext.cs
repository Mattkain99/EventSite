using EventSiteAPI.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EventSiteAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
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