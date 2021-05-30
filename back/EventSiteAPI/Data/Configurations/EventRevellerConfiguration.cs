using EventSiteAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventSiteAPI.Data.Configurations
{
    public class EventRevellerConfiguration : IEntityTypeConfiguration<EventReveller>
    {
        public void Configure(EntityTypeBuilder<EventReveller> builder)
        {
            builder.ToTable("EventReveller", schema:"EventSite");
            builder.HasKey(er => new { er.EventId, er.RevellerId });
            builder.HasOne(er => er.Event).WithMany(e => e.EventRevellers).HasForeignKey(er => er.EventId);
            builder.HasOne(er => er.Reveller).WithMany(r => r.EventRevellers).HasForeignKey(er => er.RevellerId);
        }
    }
}