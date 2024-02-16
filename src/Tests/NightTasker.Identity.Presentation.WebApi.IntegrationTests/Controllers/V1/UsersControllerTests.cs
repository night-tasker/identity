using System.Net;
using System.Net.Http.Json;
using Bogus;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NightTasker.Identity.Infrastructure.Persistence;
using NightTasker.Identity.Presentation.WebApi.Constants;
using NightTasker.Identity.Presentation.WebApi.Endpoints;
using NightTasker.Identity.Presentation.WebApi.Requests.User;
using Xunit;

namespace NightTasker.Identity.Presentation.WebApi.IntegrationTests.Controllers.V1;

public class UsersControllerTests : BaseIntegrationTests
{
    private readonly Faker _faker = new();

    [Fact]
    public async Task Post_CreateUser_SuccessLogin()
    {
        // Arrange
        var username = _faker.Random.AlphaNumeric(8);
        var email = $"{_faker.Random.AlphaNumeric(8)}@{_faker.Random.AlphaNumeric(8)}.com";
        var password = $"{_faker.Random.AlphaNumeric(8)}!";
        var createUserRequest = new CreateUserRequest(username, email, password, password);
        
        // Act
        var response = await HttpClient.PostAsJsonAsync(
            $"{ApiConstants.DefaultPrefix}/{ApiConstants.V1}/{UsersEndpoints.UsersResource}/{UsersEndpoints.UserRegister}",
            createUserRequest);
        var loginResponse = await HttpClient.PostAsJsonAsync(
            $"{ApiConstants.DefaultPrefix}/{ApiConstants.V1}/{UsersEndpoints.UsersResource}/{UsersEndpoints.UserLogin}",
            createUserRequest);
        
        // Arrange
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var scope = WebApplicationFactory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var user = await context.Users.SingleOrDefaultAsync();
        user.Should().NotBeNull();
        user!.UserName.Should().Be(username);
        user.Email.Should().Be(email);
        
        loginResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}