using EventSiteAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventSiteAPI.Data.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Event");
            builder.HasKey(e=>e.Id);
            builder.Property(e => e.Name);
            builder.Property(e => e.BeginTime);
            builder.Property(e => e.Duration);
            builder.Property(e => e.SubscribeDeadline);
            builder.Property(e => e.MaxMembers);
            builder.Property(e => e.Infos);
            builder.Property(e => e.Status);
            builder.HasOne(e => e.Place).WithMany(p => p.Events).HasForeignKey(e => e.PlaceId);
            builder.HasOne(e => e.Campus).WithMany(c => c.Events).HasForeignKey(e => e.CampusId);
            builder.HasOne(e => e.Creator).WithMany(r => r.Events).HasForeignKey(e => e.CreatorId);

        }
    }
}