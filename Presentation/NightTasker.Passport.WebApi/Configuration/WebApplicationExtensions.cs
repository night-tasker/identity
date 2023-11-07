namespace NightTasker.Passport.Presentation.Configuration;

public static class WebApplicationExtensions
{
    public static void ConfigureSwagger(this WebApplication app)
    {
        app.UseSwagger()
            .UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
    }
}