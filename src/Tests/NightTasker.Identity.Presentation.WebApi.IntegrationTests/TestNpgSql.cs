using Testcontainers.PostgreSql;

namespace NightTasker.Identity.Presentation.WebApi.IntegrationTests;

public class TestNpgSql : IAsyncDisposable
{
    public readonly PostgreSqlContainer NpgSqlContainer = new PostgreSqlBuilder()
        .WithImage("postgres:16")
        .Build();
    
    public TestNpgSql()
    {
        NpgSqlContainer.StartAsync().GetAwaiter().GetResult();
    }


    public ValueTask DisposeAsync()
    {
        return NpgSqlContainer.DisposeAsync();
    }
}