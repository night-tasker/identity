using System.Security.Claims;
using NightTasker.Identity.Application.ApplicationContracts.Identity;

namespace NightTasker.Identity.Presentation.WebApi.Services;

/// <inheritdoc />
public class IdentityService : IIdentityService
{
    public IdentityService(
        IHttpContextAccessor httpContextAccessor)
    {
        if (!httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated)
        {
            return;
        }
        
        var currentUserIdString =
            httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        if (String.IsNullOrEmpty(currentUserIdString))
        {
            return;
        }
        
        CurrentUserId = Guid.Parse(currentUserIdString);
        IsAuthenticated = true;
    }

    /// <inheritdoc />
    public Guid? CurrentUserId { get; init; }

    /// <inheritdoc />
    public bool IsAuthenticated { get; }
}