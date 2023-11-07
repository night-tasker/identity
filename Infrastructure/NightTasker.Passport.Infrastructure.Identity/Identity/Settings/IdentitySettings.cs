﻿namespace NightTasker.Passport.Infrastructure.Identity.Identity.Settings;

public class IdentitySettings
{
    public string SecretKey { get; set; } = null!;

    public string EncryptKey { get; set; } = null!;

    public string Issuer { get; set; } = null!;

    public string Audience { get; set; } = null!;

    public int NotBeforeMinutes { get; set; }
    
    public int ExpirationMinutes { get; set; }
}