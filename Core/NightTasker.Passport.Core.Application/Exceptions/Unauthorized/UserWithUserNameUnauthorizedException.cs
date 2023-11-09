﻿using NightTasker.Passport.Application.Exceptions.Base;

namespace NightTasker.Passport.Application.Exceptions.Unauthorized;

public class UserWithUserNameUnauthorizedException : UnauthorizedException
{
    public UserWithUserNameUnauthorizedException(string username) : base($"User with username: {username} not found")
    {
    }
}