using FluentValidation;
using NightTasker.Passport.Presentation.Requests.User;

namespace NightTasker.Passport.Presentation.Validators.User;

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