using Microsoft.AspNetCore.Identity;
using NightTasker.Passport.Application.Features.Users.Models;

namespace NightTasker.Passport.Application.ApplicationContracts.Identity;

public interface IUserService
{
    Task<IdentityResult> CreateUser(CreateUserDto userDto);

    Task<bool> IsUserNameExist(string username);

    Task<IdentityResult> LoginUser(LoginUserDto userDto);
}