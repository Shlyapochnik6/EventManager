using EventManager.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventManager.Persistence.EntityTypeConfigurations;

public class LocationTypeConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.HasKey(l => l.Id);
        builder.HasIndex(l => l.Id).IsUnique();
        builder.Property(l => l.CityName).IsRequired().HasMaxLength(75);
        builder.HasMany(l => l.Events)
            .WithOne(e => e.Location);
    }
}