using EventSiteAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventSiteAPI.Data.Configurations
{
    public class CampusConfiguration : IEntityTypeConfiguration<Campus>
    {
        public void Configure(EntityTypeBuilder<Campus> builder)
        {
            builder.ToTable("Campus");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name);
            builder.HasMany(c => c.Events).WithOne(e => e.Campus).HasForeignKey(e => e.CampusId);
            builder.HasMany(c => c.Revellers).WithOne(r => r.Campus).HasForeignKey(r => r.CampusId);
        }
    }
}