using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NightTasker.Passport.Application.ApplicationContracts.Jwt;
using NightTasker.Passport.Application.ApplicationContracts.Persistence;
using NightTasker.Passport.Application.Models.Jwt;
using NightTasker.Passport.Domain.Entities.User;
using NightTasker.Passport.Infrastructure.Identity.Identity.Settings;

namespace NightTasker.Passport.Infrastructure.Identity.Implementations.Services;

public class JwtService : IJwtService
{
    private readonly IdentitySettings _siteSetting;
    private readonly IUserClaimsPrincipalFactory<User> _claimsPrincipal;
    private readonly IUnitOfWork _unitOfWork;

    public JwtService(
        IOptions<IdentitySettings> siteSetting, 
        IUserClaimsPrincipalFactory<User> claimsPrincipal, 
        IUnitOfWork unitOfWork)
    {
        _siteSetting = siteSetting.Value;
        _claimsPrincipal = claimsPrincipal;
        _unitOfWork = unitOfWork;
    }
    
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
            Expires = DateTime.Now.AddMinutes(_siteSetting.ExpirationMinutes),
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
    
    public async Task<JwtAccessToken?> RefreshToken(Guid refreshTokenId, CancellationToken cancellationToken)
    {
        var refreshToken = await _unitOfWork.UserRefreshTokenRepository.TryGetValidRefreshToken(refreshTokenId, cancellationToken);
            
        if (refreshToken is null)
            return null;

        refreshToken.IsValid = false;

        await _unitOfWork.SaveChanges(cancellationToken);

        var user = await _unitOfWork.UserRefreshTokenRepository.TryGetUserByRefreshToken(refreshTokenId, cancellationToken);

        if (user is null)
            return null;

        var result = await GenerateAsync(user, cancellationToken);

        return result;
    }
    private async Task<IEnumerable<Claim>> GetClaims(User user, CancellationToken cancellationToken)
    {
        var result = await _claimsPrincipal.CreateAsync(user);
        return result.Claims;
    }
}