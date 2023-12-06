using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NightTasker.Passport.Application.ApplicationContracts.Jwt;
using NightTasker.Passport.Application.ApplicationContracts.Persistence;
using NightTasker.Passport.Application.Exceptions.Unauthorized;
using NightTasker.Passport.Application.Models.Jwt;
using NightTasker.Passport.Domain.Entities.User;
using NightTasker.Passport.Infrastructure.Identity.Identity.Settings;

namespace NightTasker.Passport.Infrastructure.Identity.Implementations.Services;

/// <inheritdoc />
public class JwtService : IJwtService
{
    private readonly IdentitySettings _siteSetting;
    private readonly IUserClaimsPrincipalFactory<User> _claimsPrincipal;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<JwtService> _logger;

    public JwtService(
        IOptions<IdentitySettings> siteSettings, 
        IUserClaimsPrincipalFactory<User> claimsPrincipal, 
        IUnitOfWork unitOfWork,
        ILogger<JwtService> logger)
    {
        _siteSetting = siteSettings.Value ?? throw new ArgumentNullException(nameof(siteSettings));
        _claimsPrincipal = claimsPrincipal ?? throw new ArgumentNullException(nameof(claimsPrincipal));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    /// <inheritdoc />
    public async Task<JwtAccessToken> GenerateAsync(User user, CancellationToken cancellationToken)
    {
        var secretKey = Encoding.UTF8.GetBytes(_siteSetting.SecretKey);
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

        var encryptionKey = Encoding.UTF8.GetBytes(_siteSetting.EncryptKey);
        var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionKey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);


        var claims = await GetClaims(user, cancellationToken);

        var descriptor = new SecurityTokenDescriptor
        {
            Issuer = _siteSetting.Issuer,
            Audience = _siteSetting.Audience,
            IssuedAt = DateTime.Now,
            NotBefore = DateTime.Now.AddMinutes(0),
            Expires = DateTime.Now.AddSeconds(_siteSetting.ExpirationMinutes),
            SigningCredentials = signingCredentials,
            EncryptingCredentials = encryptingCredentials,
            Subject = new ClaimsIdentity(claims)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var securityToken = tokenHandler.CreateJwtSecurityToken(descriptor);

        var refreshToken = await _unitOfWork.UserRefreshTokenRepository.CreateToken(user.Id, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);

        return new JwtAccessToken(securityToken, refreshToken.ToString());
    }
    
    /// <inheritdoc />
    public async Task<JwtAccessToken> RefreshToken(Guid refreshTokenId, CancellationToken cancellationToken)
    {
        var refreshToken = await _unitOfWork.UserRefreshTokenRepository.TryGetValidRefreshToken(refreshTokenId, cancellationToken);

        if (refreshToken is null)
        {
            _logger.LogError("User refresh token {RefreshToken} is not valid", refreshTokenId);
            throw new InvalidUserRefreshTokenUnauthorizedException();
        }

        refreshToken.IsValid = false;

        await _unitOfWork.SaveChanges(cancellationToken);

        var user = await _unitOfWork.UserRefreshTokenRepository.TryGetUserByRefreshToken(refreshTokenId, cancellationToken);

        if (user is null)
        {
            _logger.LogCritical(
                "User with id {UserId} for refresh token {RefreshToken} not found", 
                refreshToken.UserId,
                refreshToken.Id);
            throw new InvalidOperationException($"{typeof(User)} with id: {refreshToken.UserId} not found");
        }

        var result = await GenerateAsync(user, cancellationToken);

        return result;
    }
    private async Task<IEnumerable<Claim>> GetClaims(User user, CancellationToken cancellationToken)
    {
        var result = await _claimsPrincipal.CreateAsync(user);
        return result.Claims;
    }
}