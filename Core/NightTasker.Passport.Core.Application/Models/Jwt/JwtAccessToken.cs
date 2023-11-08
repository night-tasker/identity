using System.IdentityModel.Tokens.Jwt;

namespace NightTasker.Passport.Application.Models.Jwt;

public class JwtAccessToken
{
    public string AccessToken { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;

    public string TokenType { get; set; } = null!;

    public int ExpiresIn { get; set; }

    public JwtAccessToken(JwtSecurityToken securityToken, string refreshToken)
    {
        AccessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);
        TokenType = "Bearer";
        ExpiresIn = (int)(securityToken.ValidTo - DateTime.UtcNow).TotalSeconds;
        RefreshToken = refreshToken;
    }
}