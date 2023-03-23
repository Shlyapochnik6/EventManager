using EventManager.Application.Interfaces;
using EventManager.Domain;
using EventManager.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventManager.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<EventManagerDbContext>(options =>
            options.UseNpgsql(configuration["ConnectionStrings:DefaultConnection"], x =>
                x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));
        services.AddScoped<IEventManagerDbContext, EventManagerDbContext>();
        services.AddIdentityConfiguration();
        return services;
    }
    
    private static void AddIdentityConfiguration(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole<Guid>>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 5;
            options.User.RequireUniqueEmail = true;
            options.User.AllowedUserNameCharacters = null!;
            options.SignIn.RequireConfirmedEmail = true;
        }).AddEntityFrameworkStores<EventManagerDbContext>();
    }
}