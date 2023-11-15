using System.Net;
using System.Net.Http.Json;
using Bogus;
using FluentAssertions;
using NightTasker.Passport.Presentation.Constants;
using NightTasker.Passport.Presentation.Endpoints;
using NightTasker.Passport.Presentation.Requests.User;
using Xunit;

namespace NightTasker.Passport.Core.Application.IntegrationTests.Controllers;

public class UsersControllerTests : BaseIntegrationTests
{
    private readonly Faker _faker;

    public UsersControllerTests(
        TestNpgSql fixture) : base(fixture)
    {
        _faker = new Faker();
    }

    [Fact]
    public async Task Post_CreateUser_SuccessLogin()
    {
        // Arrange
        var username = _faker.Random.AlphaNumeric(8);
        var password = $"{_faker.Random.AlphaNumeric(8)}!";
        var createUserRequest = new CreateUserRequest(username, password, password);
        
        // Act
        var response = await HttpClient.PostAsJsonAsync(
            $"{ApiConstants.DefaultPrefix}/{ApiConstants.V1}/{UsersEndpoints.UsersResource}/{UsersEndpoints.UserRegister}",
            createUserRequest);
        var loginResponse = await HttpClient.PostAsJsonAsync(
            $"{ApiConstants.DefaultPrefix}/{ApiConstants.V1}/{UsersEndpoints.UsersResource}/{UsersEndpoints.UserLogin}",
            createUserRequest);
        
        // Arrange
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        loginResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}