using EventManager.Domain;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Application.Interfaces;

public interface IEventManagerDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Organizer> Organizers { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Speaker> Speakers { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}