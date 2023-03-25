using EventManager.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventManager.Persistence.EntityTypeConfigurations;

public class EventTypeConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasKey(e => e.Id); 
        builder.HasIndex(e => e.Id).IsUnique();
        builder.Property(e => e.Name)
            .IsRequired().HasMaxLength(100);
        builder.Property(e => e.Description)
            .IsRequired().HasMaxLength(255);
        builder.Property(e => e.Plan)
            .IsRequired().HasMaxLength(600);
        builder.HasOne(e => e.Speaker)
            .WithMany(s => s.Events);
        builder.HasOne(e => e.Location)
            .WithMany(l => l.Events);
        builder.HasOne(e => e.Organizer)
            .WithMany(u => u.Events);
    }
}