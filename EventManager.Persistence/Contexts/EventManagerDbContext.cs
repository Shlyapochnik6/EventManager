﻿using EventManager.Application.Interfaces;
using EventManager.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Persistence.Contexts;

public class EventManagerDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>,
    IEventManagerDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Speaker> Speakers { get; set; }

    public EventManagerDbContext(DbContextOptions<EventManagerDbContext> options) :
        base(options)
    {
        Database.Migrate();
    }
}