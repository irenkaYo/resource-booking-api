using FluentValidation;
using Infrastructure.DTO.Resource;

namespace Service.Validators;

public class CreateResourceValidator : AbstractValidator<CreateResourceDto>
{
    public CreateResourceValidator()
    {
        RuleFor(x => x.Name).MaximumLength(50).NotEmpty();
        
        RuleFor(x => x.Description).MaximumLength(500).NotEmpty();
        
        RuleFor(x => x.Capacity).GreaterThan(0).NotEmpty();
    }
}