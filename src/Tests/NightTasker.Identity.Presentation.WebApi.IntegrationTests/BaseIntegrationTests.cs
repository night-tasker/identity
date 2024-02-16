using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace NightTasker.Identity.Presentation.WebApi.IntegrationTests;

public abstract class BaseIntegrationTests : 
    IAsyncDisposable
{
    protected readonly TestWebApplicationFactory WebApplicationFactory;
    protected readonly HttpClient HttpClient;

    protected BaseIntegrationTests()
    {
        var testNpgSql = new TestNpgSql();
        var clientOptions = new WebApplicationFactoryClientOptions();
        WebApplicationFactory = new TestWebApplicationFactory(testNpgSql);
        HttpClient = WebApplicationFactory.CreateClient(clientOptions);
    }

    public async ValueTask DisposeAsync()
    {
        await WebApplicationFactory.DisposeAsync();
        HttpClient.Dispose();
    }
}