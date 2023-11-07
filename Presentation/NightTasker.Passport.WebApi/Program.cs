using NightTasker.Passport.Application.Configuration;
using NightTasker.Passport.Infrastructure.Configuration;
using NightTasker.Passport.Infrastructure.Identity.Configuration;
using NightTasker.Passport.Infrastructure.Identity.Identity.Settings;
using NightTasker.Passport.Presentation.Configuration;
using NightTasker.Passport.Presentation.Constants;
using NightTasker.Passport.Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<IdentitySettings>(builder.Configuration.GetSection(nameof(IdentitySettings)));
var identitySettings = builder.Configuration.GetSection(nameof(IdentitySettings)).Get<IdentitySettings>()!;

builder.Services.AddControllers();

builder.Services
    .AddApplicationServices()
    .RegisterIdentityServices(identitySettings)
    .AddPersistenceServices(builder.Configuration);

builder.Services.AddSwagger();

builder.Services.AddValidation();

builder.Services.AddDefaultCorsPolicy();

var app = builder.Build();

app.ConfigureSwagger(); 

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseCors(CorsConstants.DefaultCorsPolicyName);

app.MapControllers();

app.Run();

