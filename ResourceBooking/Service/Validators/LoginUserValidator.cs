using FluentValidation;
using Infrastructure.DTO;

namespace Service.Validators;

public class LoginUserValidator : AbstractValidator<EnterUserDto>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .MinimumLength(8);
    }
}