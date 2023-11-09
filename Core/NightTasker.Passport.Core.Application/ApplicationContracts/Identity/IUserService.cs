using Microsoft.AspNetCore.Identity;
using NightTasker.Passport.Application.Features.Users.Models;
using NightTasker.Passport.Domain.Entities.User;

namespace NightTasker.Passport.Application.ApplicationContracts.Identity;

public interface IUserService
{
    Task<IdentityResult> CreateUser(CreateUserDto userDto, CancellationToken cancellationToken);

    Task<bool> IsUserNameExist(string username, CancellationToken cancellationToken);

    Task<User> ValidateLoginUser(LoginUserDto userDto, CancellationToken cancellationToken);
}