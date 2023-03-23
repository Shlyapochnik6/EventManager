using EventManager.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventManager.Persistence.EntityTypeConfigurations;

public class SpeakerTypeConfiguration : IEntityTypeConfiguration<Speaker>
{
    public void Configure(EntityTypeBuilder<Speaker> builder)
    {
        builder.HasKey(s => s.Id);
        builder.HasIndex(s => s.Id).IsUnique();
        builder.Property(s => s.SpeakerName)
            .IsRequired().HasMaxLength(75);
        builder.HasMany(s => s.Events)
            .WithOne(e => e.Speaker);
        
    }
}