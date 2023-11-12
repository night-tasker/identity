using System.Reflection;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace NightTasker.Passport.Application.Configuration;

/// <summary>
/// Класс с методами расширения для коллекции сервисов.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Зарегистрировать сервисы слоя приложения.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Коллекция сервисов.</returns>
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.RegisterMediator();
        return services;
    }

    /// <summary>
    /// Зарегистрировать Mediator.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Коллекция сервисов.</returns>
    private static IServiceCollection RegisterMediator(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.Lifetime = ServiceLifetime.Scoped;
            options.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
        });

        return services;
    }
}