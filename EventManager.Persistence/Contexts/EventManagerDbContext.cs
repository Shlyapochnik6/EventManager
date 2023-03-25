using EventManager.Application.Interfaces;
using EventManager.Domain;
using EventManager.Persistence.EntityTypeConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Persistence.Contexts;

public class EventManagerDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>,
    IEventManagerDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Organizer> Organizers { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Speaker> Speakers { get; set; }

    public EventManagerDbContext(DbContextOptions<EventManagerDbContext> options) :
        base(options)
    {
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EventTypeConfiguration());
        modelBuilder.ApplyConfiguration(new LocationTypeConfiguration());
        modelBuilder.ApplyConfiguration(new SpeakerTypeConfiguration());
        modelBuilder.ApplyConfiguration(new UserTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OrganizerTypeConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}