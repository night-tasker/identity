using MediatR;
using NightTasker.Passport.Application.ApplicationContracts.Jwt;
using NightTasker.Passport.Application.Models.Jwt;

namespace NightTasker.Passport.Application.Features.Users.Commands.RefreshToken;

/// <summary>
/// Хэндлер команды для обновления access-токена пользователя.
/// </summary>
public class RefreshTokenCommandHandler(IJwtService jwtService) : IRequestHandler<RefreshTokenCommand, JwtAccessToken>
{
    private readonly IJwtService _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));

    public async Task<JwtAccessToken> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var token = await _jwtService.RefreshToken(request.RefreshToken.RefreshTokenId, cancellationToken);
        return token;
    }
}