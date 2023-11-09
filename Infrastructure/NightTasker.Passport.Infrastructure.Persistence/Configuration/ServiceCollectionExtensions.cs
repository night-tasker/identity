using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NightTasker.Passport.Application.ApplicationContracts.Persistence;
using NightTasker.Passport.Infrastructure.Repositories.Common;

namespace NightTasker.Passport.Infrastructure.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterPersistenceServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options
                .UseNpgsql(configuration.GetConnectionString("Database"));
        });
        return services;
    }
}