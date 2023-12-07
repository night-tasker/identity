using NightTasker.Passport.Infrastructure;
using NightTasker.Passport.Infrastructure.Configuration;

namespace NightTasker.Passport.Presentation.Configuration;

/// <summary>
/// Класс для методов расширения для WebApplication.
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    /// Конфигурация Swagger
    /// </summary>
    /// <param name="app">WebApplication</param>
    public static void ConfigureSwagger(this WebApplication app)
    {
        app.UseSwagger()
            .UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
    }

    /// <summary>
    /// Применение миграций базы данных.
    /// </summary>
    /// <param name="app">WebApplication.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public static async Task ApplyDatabaseMigrationsAsync(
        this WebApplication app,
        CancellationToken cancellationToken)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.MigrateAsync(cancellationToken);
    }
}