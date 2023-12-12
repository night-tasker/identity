using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NightTasker.Common.Core.Exceptions.Models;
using NightTasker.Identity.Application.ApplicationContracts.Identity;
using NightTasker.Identity.Application.ApplicationContracts.Jwt;
using NightTasker.Identity.Domain.Entities.User;
using NightTasker.Identity.Infrastructure.Identity.Identity.Contracts;
using NightTasker.Identity.Infrastructure.Identity.Identity.Managers;
using NightTasker.Identity.Infrastructure.Identity.Identity.Settings;
using NightTasker.Identity.Infrastructure.Identity.Identity.Store;
using NightTasker.Identity.Infrastructure.Identity.Identity.Validators;
using NightTasker.Identity.Infrastructure.Identity.Implementations.Services;

namespace NightTasker.Identity.Infrastructure.Identity.Configuration;

/// <summary>
/// Класс методов расширений для коллекции сервисов.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Зарегистрировать сервисы Infrastructure.Identity слоя.
    /// </summary>
    public static IServiceCollection RegisterIdentityServices(this IServiceCollection services, IdentitySettings identitySettings)
    {
        services.AddScoped<IUserValidator<User>, AppUserValidator>();
        services.AddScoped<UserValidator<User>, AppUserValidator>();
        
        services.AddScoped<IRoleValidator<Role>, AppRoleValidator>();
        services.AddScoped<RoleValidator<Role>, AppRoleValidator>();
        
        services.AddScoped<IRoleStore<Role>, RoleStore>();
        services.AddScoped<IUserStore<User>, AppUserStore>();

        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IJwtService, JwtService>();
        
        services.AddIdentity<User, Role>(options =>
        {
            options.Stores.ProtectPersonalData = false;

            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredUniqueChars = 0;
            options.Password.RequireUppercase = false;

            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;

            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = false;
            options.User.RequireUniqueEmail = false;
            
        }).AddUserStore<AppUserStore>()
        .AddRoleStore<RoleStore>()
        .AddUserManager<AppUserManager>()
        .AddRoleManager<AppRoleManager>()
        .AddErrorDescriber<AppErrorDescriber>()
        .AddDefaultTokenProviders()
        .AddSignInManager<AppSignInManager>()
        .AddDefaultTokenProviders();

        services.AddScoped<IAppUserManager, AppUserManager>();

        services.AddAuthorization();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            var secretKey = Encoding.UTF8.GetBytes(identitySettings.SecretKey);
            var encryptionKey = Encoding.UTF8.GetBytes(identitySettings.EncryptKey);

            var validationParameters = new TokenValidationParameters
            {
                ClockSkew = TimeSpan.Zero,
                RequireSignedTokens = true,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ValidateAudience = true,
                ValidAudience = identitySettings.Audience,

                ValidateIssuer = true,
                ValidIssuer = identitySettings.Issuer,

                TokenDecryptionKey = new SymmetricSecurityKey(encryptionKey),
            };
            
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = validationParameters;
            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                    logger.LogError("Authentication failed.");
                    return Task.CompletedTask;
                },
                OnTokenValidated = async context =>
                {
                    var signInManager = context.HttpContext.RequestServices.GetRequiredService<AppSignInManager>();

                    var claimsIdentity = context.Principal?.Identity as ClaimsIdentity;
                    if (claimsIdentity?.Claims?.Any() != true)
                        context.Fail("This token has no claims.");

                    var securityStamp =
                        claimsIdentity?.FindFirst(new ClaimsIdentityOptions().SecurityStampClaimType)?.Value;
                    if (securityStamp is null)
                        context.Fail("This token has no security stamp");

                    var validatedUser = await signInManager.ValidateSecurityStampAsync(context.Principal);
                    if (validatedUser == null)
                        context.Fail("Token security stamp is not valid.");

                },
                OnChallenge = async context =>
                {
                    var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                    logger.LogError("OnChallenge error");
                    context.HandleResponse();
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    var response = new ErrorDetails { TraceId = context.HttpContext.TraceIdentifier };
                    
                    if (context.AuthenticateFailure is SecurityTokenExpiredException)
                    {
                        response.Message = "Token is expired. refresh your token";
                    }
                    else if (context.AuthenticateFailure != null)
                    {
                        response.Message = "Token is Not Valid";
                    }
                    else
                    {
                        response.Message = "Invalid access token. Please login";
                    }

                    await context.Response.WriteAsJsonAsync(response);
                },
                OnForbidden = async context =>
                {
                    var response = new ErrorDetails
                        { TraceId = context.HttpContext.TraceIdentifier, Message = ErrorDetails.DefaultErrorMessage };
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                   await context.Response.WriteAsJsonAsync(response);
                }
            };
        });

        return services;
    }
}