using NightTasker.Passport.Application.Configuration;
using NightTasker.Passport.Infrastructure.Configuration;
using NightTasker.Passport.Infrastructure.Identity.Configuration;
using NightTasker.Passport.Infrastructure.Identity.Identity.Settings;
using NightTasker.Passport.Presentation.Configuration;
using NightTasker.Passport.Presentation.Constants;
using NightTasker.Passport.Presentation.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.Configure<IdentitySettings>(builder.Configuration.GetSection(nameof(IdentitySettings)));
var identitySettings = builder.Configuration.GetSection(nameof(IdentitySettings)).Get<IdentitySettings>()!;

builder.Services.AddControllers();

builder.Services
    .RegisterApplicationServices()
    .RegisterIdentityServices(identitySettings)
    .RegisterPersistenceServices(builder.Configuration)
    .RegisterApiServices();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSwagger();
}

builder.Services.AddValidation();

builder.Services.AddDefaultCorsPolicy();

var app = builder.Build();

await app.ApplyDatabaseMigrationsAsync(CancellationToken.None);

app.UseSerilogRequestLogging();

if (builder.Environment.IsDevelopment())
{
    app.ConfigureSwagger(); 
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseCors(CorsConstants.DefaultCorsPolicyName);

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }