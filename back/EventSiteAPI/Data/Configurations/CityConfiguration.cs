using EventSiteAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventSiteAPI.Data.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("City", schema:"EventSite");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name);
            builder.Property(c => c.ZipCode).HasColumnType("char(5)");
            builder.HasMany(c => c.Places).WithOne(p => p.City).HasForeignKey(p => p.CityId);
        }
    }
}