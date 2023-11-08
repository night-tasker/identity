using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NightTasker.Passport.Application.ApplicationContracts.Identity;
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
    public Task<IdentityResult> CreateUser(CreateUserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        return _userManager.CreateAsync(user);
    }

    public Task<bool> IsUserNameExist(string username)
    {
        return _userManager.Users.AnyAsync(x => x.UserName != null && x.UserName.Equals(username));
    }

    public async Task<IdentityResult> LoginUser(LoginUserDto userDto)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userDto.UserName);
        if (user is null)
        {
            throw new UserWithUserNameUnauthorized(userDto.UserName);
        }

        if (!await _userManager.CheckPasswordAsync(user, userDto.Password))
        {
            throw new WrongUserPasswordUnauthorizedException(userDto.UserName);
        }

        throw new NotImplementedException();
    }
}