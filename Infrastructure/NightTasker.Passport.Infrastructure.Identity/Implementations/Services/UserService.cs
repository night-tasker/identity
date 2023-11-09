using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NightTasker.Passport.Application.ApplicationContracts.Identity;
using NightTasker.Passport.Application.Exceptions.BadRequest;
using NightTasker.Passport.Application.Exceptions.Unauthorized;
using NightTasker.Passport.Application.Features.Users.Models;
using NightTasker.Passport.Domain.Entities.User;
using NightTasker.Passport.Infrastructure.Identity.Identity.Managers;

namespace NightTasker.Passport.Infrastructure.Identity.Implementations.Services;

public class UserService : IUserService
{
    private readonly AppUserManager _userManager;
    private readonly IMapper _mapper;

    public UserService(
        AppUserManager userManager,
        IMapper mapper)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    public async Task<IdentityResult> CreateUser(CreateUserDto userDto, CancellationToken cancellationToken)
    {
        if (await IsUserNameExist(userDto.UserName, cancellationToken))
        {
            throw new UserNameExistsBadRequestException(userDto.UserName);
        }
        
        var user = _mapper.Map<User>(userDto);
        var createIdentityResult = await _userManager.CreateAsync(user);
        var addPasswordIdentityResult = await _userManager.AddPasswordAsync(user, userDto.Password);
        
        if (!(createIdentityResult.Succeeded && addPasswordIdentityResult.Succeeded))
        {
            var result = IdentityResult.Failed(
                createIdentityResult.Errors.Concat(addPasswordIdentityResult.Errors).ToArray());
            return result;
        }
        
        return IdentityResult.Success;
    }

    public Task<bool> IsUserNameExist(string? username, CancellationToken cancellationToken)
    {
        return _userManager.Users.AnyAsync(x => x.UserName != null && x.UserName.Equals(username), cancellationToken);
    }

    public async Task<User> ValidateLoginUser(LoginUserDto userDto, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userDto.UserName, cancellationToken);
        if (user is null)
        {
            throw new UserWithUserNameUnauthorizedException(userDto.UserName);
        }

        if (!await _userManager.CheckPasswordAsync(user, userDto.Password))
        {
            throw new WrongUserPasswordUnauthorizedException(userDto.UserName);
        }

        return user;
    }
}