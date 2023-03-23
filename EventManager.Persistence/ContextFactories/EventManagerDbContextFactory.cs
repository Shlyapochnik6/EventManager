using System.Reflection;
using EventManager.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EventManager.Persistence.ContextFactories;

public class EventManagerDbContextFactory : IDesignTimeDbContextFactory<EventManagerDbContext>
{
    private const string _currentDirectoryName = "EventManager.Persistence";
    private const string _mainDirectoryName = "EventManager.WebAPI";
    
    public EventManagerDbContext CreateDbContext(string[] args)
    {
        var connectionString = GetConnectionString();
        var optionsBuilder = new DbContextOptionsBuilder<EventManagerDbContext>();
        optionsBuilder.UseNpgsql(connectionString);
        return new EventManagerDbContext(optionsBuilder.Options);
    }

    private static string GetConnectionString()
    {
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!
            .Replace(_currentDirectoryName, _mainDirectoryName);
        var configuration = new ConfigurationBuilder()
            .SetBasePath(path)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();  
        return configuration["ConnectionStrings:DefaultConnection"] ??
               throw new NullReferenceException("The connection string is empty");
    }
}