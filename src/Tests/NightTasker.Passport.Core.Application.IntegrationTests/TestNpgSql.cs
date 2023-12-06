using Npgsql;
using Testcontainers.PostgreSql;
using Xunit;

namespace NightTasker.Passport.Core.Application.IntegrationTests;

public class TestNpgSql : IAsyncLifetime
{
    public readonly PostgreSqlContainer NpgSqlContainer = new PostgreSqlBuilder()
        .WithImage("postgres:16")
        .Build();

    public Task InitializeAsync()
    {
        return NpgSqlContainer.StartAsync();
    }

    public Task DisposeAsync()
    {
        return NpgSqlContainer.DisposeAsync().AsTask();
    }
}