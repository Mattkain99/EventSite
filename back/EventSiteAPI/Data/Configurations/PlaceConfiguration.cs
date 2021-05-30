using EventSiteAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventSiteAPI.Data.Configurations
{
    public class PlaceConfiguration : IEntityTypeConfiguration<Place>
    {
        public void Configure(EntityTypeBuilder<Place> builder)
        {
            builder.ToTable("Place", schema:"EventSite");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name);
            builder.Property(p => p.Street);
            builder.Property(p => p.CityId);
            builder.HasOne(p => p.City).WithMany(c=>c.Places).HasForeignKey(p=>p.CityId);
            builder.HasMany(p => p.Events).WithOne(e => e.Place).HasForeignKey(e => e.PlaceId); //
        }
    }
}