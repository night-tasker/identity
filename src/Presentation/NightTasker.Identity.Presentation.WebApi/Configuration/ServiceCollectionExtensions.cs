using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using Microsoft.OpenApi.Models;
using NightTasker.Identity.Application.ApplicationContracts.Identity;
using NightTasker.Identity.Presentation.WebApi.Constants;
using NightTasker.Identity.Presentation.WebApi.Services;

namespace NightTasker.Identity.Presentation.WebApi.Configuration;

/// <summary>
/// Класс для методов расширения для Presentation слоя.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Зарегистрировать сервисы АПИ слоя.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public static void RegisterApiServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddMapper();
    }
    
    /// <summary>
    /// Добавить Swagger.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo 
                { Title = "night-tasker-passport", Version = "1.0", Description = "night tasker passport application" }
            );
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
            
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        });
    }

    /// <summary>
    /// Добавить валидацию.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public static void AddValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<Program>();
        services.AddFluentValidationAutoValidation(configuration =>
        {
            configuration.DisableDataAnnotationsValidation = true;
        });
    }

    /// <summary>
    /// Добавить дефолтные настройки CORS.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public static IServiceCollection AddDefaultCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(policy =>
        {
            policy.AddPolicy(CorsConstants.DefaultCorsPolicyName, builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
        });
        
        return services;
    }

    /// <summary>
    /// Добавить Маппер.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    private static IServiceCollection AddMapper(this IServiceCollection services)
    {
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        typeAdapterConfig.Scan(
            typeof(ServiceCollectionExtensions).Assembly,
            typeof(Identity.Application.Configuration.ServiceCollectionExtensions).Assembly);
        typeAdapterConfig.RequireExplicitMapping = true;
        var mapperConfig = new Mapper(typeAdapterConfig);
        services.AddSingleton<IMapper>(mapperConfig);
        return services;
    }
}