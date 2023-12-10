using System.Security.Claims;
using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NightTasker.Identity.Presentation.WebApi.Services;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace NightTasker.Identity.Presentation.WebApi.UnitTests.Services;

[TestFixture]
public class IdentityServiceTests
{
    private IHttpContextAccessor _httpContextAccessor = null!;
    private Faker _faker = null!;

    [SetUp]
    public void Setup()
    {
        _httpContextAccessor = Substitute.For<IHttpContextAccessor>();
        _faker = new();
    }

    [Test]
    public void Constructor_UserIsNotAuthenticatedByIdentity_IsAuthenticatedIsFalseAndCurrentUserIdIsNull()
    {
        // Arrange
        _httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated.Returns(false);
        
        // Act
        var sut = new IdentityService(_httpContextAccessor);
        
        // Assert
        sut.IsAuthenticated.Should().Be(false);
        sut.CurrentUserId.Should().BeNull();
    }
    
    [Test]
    public void Constructor_NameIdentityClaimIsNull_IsAuthenticatedIsFalseAndCurrentUserIdIsNull()
    {
        // Arrange
        _httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated.Returns(true);
        _httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value
            .ReturnsNull();
        
        // Act
        var sut = new IdentityService(_httpContextAccessor);
        
        // Assert
        sut.IsAuthenticated.Should().Be(false);
        sut.CurrentUserId.Should().BeNull();
    }
    
    [Test]
    public void Constructor_NameIdentityClaimIsGuid_IsAuthenticatedIsTrueAndCurrentUserIdIsGuid()
    {
        // Arrange
        var userId = _faker.Random.Guid();
        _httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated.Returns(true);
        _httpContextAccessor.HttpContext!.User.Claims
            .Returns(new []{new Claim(ClaimTypes.NameIdentifier, userId.ToString())});
        
        // Act
        var sut = new IdentityService(_httpContextAccessor);
        
        // Assert
        sut.IsAuthenticated.Should().Be(true);
        sut.CurrentUserId.Should().Be(userId);
    }
}