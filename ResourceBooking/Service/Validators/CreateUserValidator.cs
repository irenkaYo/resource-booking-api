using Domain.Models;
using Infrastructure.DTO;
using FluentValidation;

namespace Service.Validators;

public class CreateUserValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Name).MaximumLength(50).NotEmpty();
        
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .MinimumLength(8);
        
        RuleFor(x => x.Role)
            .Must(role => Enum.TryParse<UserRole>(role, true, out _));
    }
}