namespace NightTasker.Passport.Application.ApplicationContracts.Identity;

public interface IIdentityService
{
    Guid? CurrentUserId { get; }
    
    bool IsAuthenticated { get; }
}