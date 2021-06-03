using EventSiteAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventSiteAPI.Data.Configurations
{
    public class RevellerConfiguration : IEntityTypeConfiguration<Reveller>
    {
        public void Configure(EntityTypeBuilder<Reveller> builder)
        {
            builder.ToTable("Reveller", schema:"EventSite");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.FirstName);
            builder.Property(r => r.LastName);
            builder.Property(r => r.IsAdmin);
            builder.Property(r => r.IsActive);
            builder.Property(r => r.CampusId);
            builder.HasOne(r => r.Campus).WithMany(c => c.Revellers).HasForeignKey(r => r.CampusId);
            builder.HasMany(r => r.Events).WithOne(e => e.Creator).HasForeignKey(e => e.CreatorId);

        }
    }
}