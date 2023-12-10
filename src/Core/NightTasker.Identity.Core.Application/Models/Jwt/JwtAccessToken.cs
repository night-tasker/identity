using System.IdentityModel.Tokens.Jwt;

namespace NightTasker.Identity.Application.Models.Jwt;

/// <summary>
/// JWT токен доступа.
/// </summary>
public class JwtAccessToken(JwtSecurityToken securityToken, string refreshToken)
{
    /// <summary>
    /// Токен доступа.
    /// </summary>
    public string AccessToken { get; init; } = new JwtSecurityTokenHandler().WriteToken(securityToken);

    /// <summary>
    /// Токен для обновления.
    /// </summary>
    public string RefreshToken { get; init; } = refreshToken;

    /// <summary>
    /// Тип токена.
    /// </summary>
    public string TokenType { get; init; } = "Bearer";

    /// <summary>
    /// Срок истечения.
    /// </summary>
    public int ExpiresIn { get; init; } = (int)(securityToken.ValidTo - DateTime.UtcNow).TotalSeconds;
}