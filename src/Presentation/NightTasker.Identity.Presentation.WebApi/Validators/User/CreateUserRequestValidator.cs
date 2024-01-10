using System.Data;
using FluentValidation;
using NightTasker.Identity.Presentation.WebApi.Requests.User;

namespace NightTasker.Identity.Presentation.WebApi.Validators.User;

/// <summary>
/// Валидатор для CreateUserRequest
/// </summary>
public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .MinimumLength(5);
        
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8);

        RuleFor(x => x.ConfirmPassword)
            .Matches(x => x.Password);
    }
}