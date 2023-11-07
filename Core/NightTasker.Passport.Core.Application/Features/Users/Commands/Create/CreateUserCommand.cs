using MediatR;
using Microsoft.AspNetCore.Identity;
using NightTasker.Passport.Application.Features.Users.Models;

namespace NightTasker.Passport.Application.Features.Users.Commands.Create;

public record CreateUserCommand(CreateUserDto User) : IRequest<IdentityResult>;