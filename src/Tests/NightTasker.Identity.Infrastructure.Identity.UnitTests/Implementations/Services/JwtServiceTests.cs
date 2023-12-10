using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NightTasker.Identity.Application.ApplicationContracts.Jwt;
using NightTasker.Identity.Application.ApplicationContracts.Persistence;
using NightTasker.Identity.Application.Exceptions.Unauthorized;
using NightTasker.Identity.Domain.Entities.User;
using NightTasker.Identity.Infrastructure.Identity.Identity.Settings;
using NightTasker.Identity.Infrastructure.Identity.Implementations.Services;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace NightTasker.Identity.Infrastructure.Identity.UnitTests.Implementations.Services;

[TestFixture]
public class JwtServiceTests
{
    private IJwtService _sut = null!;
    private IUnitOfWork _unitOfWork = null!;
    private Faker _faker = null!;
    private IOptions<IdentitySettings> _identitySettings = null!;

    [SetUp]
    public void Setup()
    {
        _identitySettings = Substitute.For<IOptions<IdentitySettings>>();
        _identitySettings.Value.Returns(new IdentitySettings());
        _sut = new JwtService(
            _identitySettings,
            Substitute.For<IUserClaimsPrincipalFactory<User>>(),
            _unitOfWork = Substitute.For<IUnitOfWork>(),
            Substitute.For<ILogger<JwtService>>());
        _faker = new Faker();
    }

    [Test]
    public void RefreshToken_InvalidRefreshTokenId_InvalidUserRefreshTokenUnauthorizedException()
    {
        // Arrange
        var refreshToken = _faker.Random.Guid();
        _unitOfWork.UserRefreshTokenRepository.TryGetValidRefreshToken(refreshToken, Arg.Any<CancellationToken>())
            .ReturnsNull();
        
        // Act
        async Task TestDelegate() => await _sut.RefreshToken(refreshToken, CancellationToken.None);

        // Assert
        Assert.ThrowsAsync<InvalidUserRefreshTokenUnauthorizedException>(TestDelegate);
    }
    
    [Test]
    public void RefreshToken_InvalidUserId_InvalidOperationException()
    {
        // Arrange
        var refreshToken = _faker.Random.Guid();
        _unitOfWork.UserRefreshTokenRepository.TryGetValidRefreshToken(refreshToken, Arg.Any<CancellationToken>())
            .Returns(new UserRefreshToken());
        _unitOfWork.UserRefreshTokenRepository.TryGetUserByRefreshToken(refreshToken, Arg.Any<CancellationToken>())
            .ReturnsNull();
        
        // Act
        async Task TestDelegate() => await _sut.RefreshToken(refreshToken, CancellationToken.None);

        // Assert
        Assert.ThrowsAsync<InvalidOperationException>(TestDelegate);
    }
}