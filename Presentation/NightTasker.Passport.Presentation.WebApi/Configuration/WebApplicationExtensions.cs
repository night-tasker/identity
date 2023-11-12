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
}