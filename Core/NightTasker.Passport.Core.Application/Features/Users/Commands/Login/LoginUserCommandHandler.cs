using MediatR;
using NightTasker.Passport.Application.ApplicationContracts.Identity;
using NightTasker.Passport.Application.ApplicationContracts.Jwt;
using NightTasker.Passport.Application.Models.Jwt;

namespace NightTasker.Passport.Application.Features.Users.Commands.Login;

/// <summary>
/// Хэндлер для команды на вход пользователя.
/// </summary>
public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, JwtAccessToken>
{
    private readonly IJwtService _jwtService;
    private readonly IUserService _userService;

    public LoginUserCommandHandler(
        IJwtService jwtService,
        IUserService userManager)
    {
        _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
        _userService = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }
    
    public async Task<JwtAccessToken> Handle(
        LoginUserCommand request, 
        CancellationToken cancellationToken)
    {
        var user = await _userService.ValidateLoginUser(request.UserDto, cancellationToken);
        var accessToken = await _jwtService.GenerateAsync(user, cancellationToken);
        return accessToken;
    }
}