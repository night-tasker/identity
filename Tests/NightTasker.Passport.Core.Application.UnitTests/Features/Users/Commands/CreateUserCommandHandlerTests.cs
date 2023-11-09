using Bogus;
using Microsoft.AspNetCore.Identity;
using NightTasker.Passport.Application.ApplicationContracts.Identity;
using NightTasker.Passport.Application.Exceptions.BadRequest;
using NightTasker.Passport.Application.Features.Users.Commands.Create;
using NightTasker.Passport.Application.Features.Users.Models;
using NSubstitute;

namespace NightTasker.Passport.Core.Application.UnitTests.Features.Users.Commands;

[TestFixture]
public class CreateUserCommandHandlerTests
{
    private IUserService _userService = null!;
    private Faker _faker = null!;
    private CreateUserCommandHandler _sut = null!;

    [SetUp]
    public void Setup()
    {
        _userService = Substitute.For<IUserService>();
        _faker = new Faker();
        _sut = new CreateUserCommandHandler(_userService);
    }

    [TestCase]
    public void Handle_UserNameExists_UserNameExistsBadRequestException()
    {
        // Arrange
        _userService
            .IsUserNameExist(Arg.Any<string>(), CancellationToken.None)
            .Returns(true);
        var password = _faker.Random.String();
        var createUserDto = new CreateUserDto(_faker.Random.String(), password);
        var command = new CreateUserCommand(createUserDto);
        
        // Act
        async Task TestMethodWrapper() => await _sut.Handle(command, CancellationToken.None);

        // Assert
        Assert.ThrowsAsync<UserNameExistsBadRequestException>(TestMethodWrapper);
    }
    
    [TestCase]
    public async Task Handle_UserNameDoesNotExist_SuccessIdentityResult()
    {
        // Arrange
        _userService
            .IsUserNameExist(Arg.Any<string>(), CancellationToken.None)
            .Returns(false);
        var identityResult = IdentityResult.Success;
        var password = _faker.Random.String();
        var createUserDto = new CreateUserDto(_faker.Random.String(), password);
        var command = new CreateUserCommand(createUserDto);
        _userService.CreateUser(createUserDto, CancellationToken.None)
            .Returns(identityResult);
        
        // Act
        var result = await _sut.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.Succeeded);
    }
}