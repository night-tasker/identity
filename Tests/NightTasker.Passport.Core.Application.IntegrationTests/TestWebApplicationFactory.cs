using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
}