﻿using MediatR;
using NightTasker.Identity.Application.ApplicationContracts.Identity;
using NightTasker.Identity.Application.ApplicationContracts.Jwt;
using NightTasker.Identity.Application.Models.Jwt;

namespace NightTasker.Identity.Application.Features.Users.Commands.Login;

/// <summary>
/// Хэндлер для команды на вход пользователя.
/// </summary>
public class LoginUserCommandHandler(
    IJwtService jwtService,
    IUserService userManager) : IRequestHandler<LoginUserCommand, JwtAccessToken>
{
    private readonly IJwtService _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
    private readonly IUserService _userService = userManager ?? throw new ArgumentNullException(nameof(userManager));

    public async Task<JwtAccessToken> Handle(
        LoginUserCommand request, 
        CancellationToken cancellationToken)
    {
        var user = await _userService.ValidateLoginUser(request.UserDto, cancellationToken);
        var accessToken = await _jwtService.GenerateAsync(user, cancellationToken);
        return accessToken;
    }
}