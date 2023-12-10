using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NightTasker.Passport.Application.ApplicationContracts.Identity;
using NightTasker.Passport.Application.Exceptions.BadRequest;

namespace NightTasker.Passport.Application.Features.Users.Commands.Create;

/// <summary>
/// Хэндлер для команды на создание пользователя.
/// </summary>
public class CreateUserCommandHandler(IUserService userService) : IRequestHandler<CreateUserCommand, IdentityResult>
{
    private readonly IUserService _userService = userService ?? throw new ArgumentNullException(nameof(userService));

    public async Task<IdentityResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var identityResult = await _userService.CreateUser(request.User, CancellationToken.None);
        return identityResult;
    }
}