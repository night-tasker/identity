using NightTasker.Common.Core.Exceptions.Middlewares;
using NightTasker.Identity.Application.Configuration;
using NightTasker.Identity.Infrastructure.Identity.Configuration;
using NightTasker.Identity.Infrastructure.Identity.Identity.Settings;
using NightTasker.Identity.Infrastructure.Persistence.Configuration;
using NightTasker.Identity.Presentation.WebApi.Configuration;
using NightTasker.Identity.Presentation.WebApi.Constants;
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