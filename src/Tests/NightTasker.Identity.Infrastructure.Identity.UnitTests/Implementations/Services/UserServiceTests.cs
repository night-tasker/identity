using Bogus;
using FluentAssertions;
using MapsterMapper;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MockQueryable.NSubstitute;
using NightTasker.Identity.Application.Exceptions.BadRequest;
using NightTasker.Identity.Application.Features.Users.Models;
using NightTasker.Identity.Domain.Entities.User;
using NightTasker.Identity.Infrastructure.Identity.Identity.Contracts;
using NightTasker.Identity.Infrastructure.Identity.Implementations.Services;
using NSubstitute;

namespace NightTasker.Identity.Infrastructure.Identity.UnitTests.Implementations.Services;

[TestFixture]
public class UserServiceTests
{
    private UserService _sut = null!;
    private IAppUserManager _appUserManager = null!;
    private IMapper _mapper = null!;
    private ILogger<UserService> _logger = null!;
    private Faker _faker = null!;

    [SetUp]
    public void Setup()
    {
        _appUserManager = Substitute.For<IAppUserManager>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<UserService>>();
        _sut = new UserService(_appUserManager, _mapper, _logger, Substitute.For<IPublishEndpoint>());
        _faker = new Faker();
    }

    [Test]
    public void CreateUser_UserNameExists_UserNameExistsBadRequestException()
    {
        // Arrange
        var userName = _faker.Random.String();
        var password = _faker.Random.String();
        var user = new User(userName);
        var users = new List<User> { user };
        var mockUsers = users.BuildMock();
        _appUserManager.Users.Returns(mockUsers);
        var createUserDto = new CreateUserDto(userName, password);

        // Act
        async Task TestDelegate() => await _sut.CreateUser(createUserDto, CancellationToken.None);

        // Assert
        Assert.ThrowsAsync<UserNameExistsBadRequestException>(TestDelegate);
    }
    
    [Test]
    public async Task CreateUser_UserAndPasswordHaveErrors_AllErrors()
    {
        // Arrange
        var userName = _faker.Random.String();
        var password = _faker.Random.String();
        var mockUsers = new List<User>().BuildMock();
        _appUserManager.Users.Returns(mockUsers);
        var createUserDto = new CreateUserDto(userName, password);

        var createUserError = new IdentityError()
            { Code = _faker.Random.Guid().ToString(), Description = _faker.Random.String() };
        var createUserErrors = new [] { createUserError };
        
        var passwordError = new IdentityError()
            { Code = _faker.Random.Guid().ToString(), Description = _faker.Random.String() };
        var passwordErrors = new[] { passwordError };
        
        _appUserManager.CreateAsync(Arg.Any<User>())
            .Returns(IdentityResult.Failed(createUserErrors));

        _appUserManager.AddPasswordAsync(Arg.Any<User>(), Arg.Any<string>())
            .Returns(IdentityResult.Failed(passwordErrors));

        // Act
        var result =  await _sut.CreateUser(createUserDto, CancellationToken.None);

        // Assert
        result.Succeeded.Should().BeFalse();
        result.Errors.Contains(createUserError).Should().BeTrue();
        result.Errors.Contains(passwordError).Should().BeTrue();
    }
    
    [Test]
    public async Task CreateUser_SuccessUserCreationAndAddingPassword_SucceededResult()
    {
        // Arrange
        var userName = _faker.Random.String();
        var password = _faker.Random.String();
        var mockUsers = new List<User>().BuildMock();
        _appUserManager.Users.Returns(mockUsers);
        var createUserDto = new CreateUserDto(userName, password);
        _appUserManager.CreateAsync(Arg.Any<User>())
            .Returns(IdentityResult.Success);

        _appUserManager.AddPasswordAsync(Arg.Any<User>(), Arg.Any<string>())
            .Returns(IdentityResult.Success);

        // Act
        var result =  await _sut.CreateUser(createUserDto, CancellationToken.None);

        // Assert
        result.Succeeded.Should().BeTrue();
    }
}