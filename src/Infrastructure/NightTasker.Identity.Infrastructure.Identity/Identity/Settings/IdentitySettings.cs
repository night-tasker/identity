namespace NightTasker.Identity.Infrastructure.Identity.Identity.Settings;

/// <summary>
/// Конфигурационные настройки для Identity.
/// </summary>
public class IdentitySettings
{
    public string SecretKey { get; set; } = null!;

    public string EncryptKey { get; set; } = null!;

    public string Issuer { get; set; } = null!;

    public string Audience { get; set; } = null!;

    public int NotBeforeMinutes { get; set; }
    
    public int ExpirationMinutes { get; set; }
}