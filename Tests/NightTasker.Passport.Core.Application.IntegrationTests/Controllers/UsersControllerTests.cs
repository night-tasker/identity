using System.Net;
using System.Net.Http.Json;
using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NightTasker.Passport.Application.ApplicationContracts.Persistence;
using NightTasker.Passport.Infrastructure;
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
    public async Task Post_CreateUser_UserCreated()
    {
        // Arrange
        var username = _faker.Random.String();
        var password = _faker.Random.String();
        var createUserRequest = new CreateUserRequest(username, password, password);
        
        // Act
        var response = await HttpClient.PostAsJsonAsync(UsersEndpoints.UsersResource + "/" + UsersEndpoints.UserRegister, createUserRequest);
        var loginResponse = await HttpClient.PostAsJsonAsync(UsersEndpoints.UsersResource + "/" + UsersEndpoints.UserLogin, createUserRequest);
        
        // Arrange
        loginResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}