using System.IdentityModel.Tokens.Jwt;

namespace NightTasker.Passport.Application.Models.Jwt;

/// <summary>
/// JWT токен доступа.
/// </summary>
public class JwtAccessToken
{
    /// <summary>
    /// Токен доступа.
    /// </summary>
    public string AccessToken { get; init; }

    /// <summary>
    /// Токен для обновления.
    /// </summary>
    public string RefreshToken { get; init; }

    /// <summary>
    /// Тип токена.
    /// </summary>
    public string TokenType { get; init; }

    /// <summary>
    /// Срок истечения.
    /// </summary>
    public int ExpiresIn { get; init; }

    public JwtAccessToken(JwtSecurityToken securityToken, string refreshToken)
    {
        AccessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);
        TokenType = "Bearer";
        ExpiresIn = (int)(securityToken.ValidTo - DateTime.UtcNow).TotalSeconds;
        RefreshToken = refreshToken;
    }
}