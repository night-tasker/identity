using MediatR;
using NightTasker.Passport.Application.Features.Users.Models;
using NightTasker.Passport.Application.Models.Jwt;

namespace NightTasker.Passport.Application.Features.Users.Commands.LoginUser;

public record LoginUserCommand(LoginUserDto UserDto) : IRequest<JwtAccessToken>;