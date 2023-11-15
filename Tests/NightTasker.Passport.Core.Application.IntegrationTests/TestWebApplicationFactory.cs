using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NightTasker.Passport.Infrastructure;

namespace NightTasker.Passport.Core.Application.IntegrationTests;

public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string _connectionString;

    public TestWebApplicationFactory(TestNpgSql databaseFixture)
    {
        _connectionString = databaseFixture.NpgSqlContainer.GetConnectionString();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.Remove(services.SingleOrDefault(service =>
                typeof(DbContextOptions<ApplicationDbContext>) == service.ServiceType)!);
            services.Remove(services.SingleOrDefault(service =>
                typeof(DbConnection) == service.ServiceType)!);
            services.AddDbContext<ApplicationDbContext>((_, option) => option.UseNpgsql(_connectionString));
        });
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        var host = base.CreateHost(builder);
        using var scope = host.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
        return host;
    }
}