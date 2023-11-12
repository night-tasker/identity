using FluentValidation;
using NightTasker.Passport.Presentation.Requests.User;

namespace NightTasker.Passport.Presentation.Validators.User;

/// <summary>
/// Валидатор для LoginUserRequest
/// </summary>
public class LoginUserRequestValidator : AbstractValidator<LoginUserRequest>
{
    public LoginUserRequestValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .MinimumLength(5);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8);
    }
}