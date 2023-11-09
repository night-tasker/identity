using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using NightTasker.Passport.Application.ApplicationContracts.Identity;
using NightTasker.Passport.Presentation.Constants;
using NightTasker.Passport.Presentation.Services;

namespace NightTasker.Passport.Presentation.Configuration;

public static class ServiceCollectionExtensions
{
    public static void RegisterApiServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IIdentityService, IdentityService>();
    }
    
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

    public static void AddValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<Program>();
        services.AddFluentValidationAutoValidation(configuration =>
        {
            configuration.DisableDataAnnotationsValidation = true;
        });
    }

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
}