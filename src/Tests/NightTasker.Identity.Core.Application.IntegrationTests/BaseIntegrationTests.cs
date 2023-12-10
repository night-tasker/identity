using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace NightTasker.Identity.Core.Application.IntegrationTests;

public abstract class BaseIntegrationTests : 
    IClassFixture<TestNpgSql>, 
    IAsyncDisposable
{
    protected readonly TestWebApplicationFactory WebApplicationFactory;
    protected readonly HttpClient HttpClient;

    protected BaseIntegrationTests(
        TestNpgSql fixture)
    {
        var clientOptions = new WebApplicationFactoryClientOptions();
        WebApplicationFactory = new TestWebApplicationFactory(fixture);
        HttpClient = WebApplicationFactory.CreateClient(clientOptions);
    }

    public async ValueTask DisposeAsync()
    {
        await WebApplicationFactory.DisposeAsync();
        HttpClient.Dispose();
    }
}