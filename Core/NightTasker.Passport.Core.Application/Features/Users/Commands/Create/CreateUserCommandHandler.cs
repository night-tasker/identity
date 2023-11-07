using MediatR;
using Microsoft.AspNetCore.Identity;
using NightTasker.Passport.Application.Contracts.Identity;
using NightTasker.Passport.Application.Exceptions.BadRequest;

namespace NightTasker.Passport.Application.Features.Users.Commands.Create;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IdentityResult>
{
    private readonly IUserService _userService;

    public CreateUserCommandHandler(IUserService userService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }
    
    public async Task<IdentityResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userService.IsUserNameExist(request.User.UserName))
        {
            throw new UserNameExistsBadRequestException(request.User.UserName);
        }

        var identityResult = await _userService.CreateUser(request.User);
        return identityResult;
    }
}