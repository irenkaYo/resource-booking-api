using FluentValidation;
using Service.DTO;

namespace Service.Validators;

public class NameDtoValidator<T> : AbstractValidator<T> where T : INameDto
{
    public NameDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);
    }
}