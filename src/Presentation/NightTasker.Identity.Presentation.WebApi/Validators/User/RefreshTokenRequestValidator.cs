using FluentValidation;
using NightTasker.Identity.Presentation.WebApi.Requests.User;

namespace NightTasker.Identity.Presentation.WebApi.Validators.User;

/// <summary>
/// Валидатор для RefreshTokenRequest
/// </summary>
public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
{
    public RefreshTokenRequestValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotNull()
            .NotEqual(Guid.Empty);
    }
}