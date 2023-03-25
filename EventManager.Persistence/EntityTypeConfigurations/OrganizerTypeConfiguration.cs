using EventManager.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventManager.Persistence.EntityTypeConfigurations;

public class OrganizerTypeConfiguration : IEntityTypeConfiguration<Organizer>
{
    public void Configure(EntityTypeBuilder<Organizer> builder)
    {
        builder.HasKey(s => s.Id); 
        builder.HasIndex(s => s.Id).IsUnique();
        builder.Property(s => s.OrganizerName)
            .IsRequired().HasMaxLength(75);
        builder.HasMany(s => s.Events)
            .WithOne(e => e.Organizer);
    }
}