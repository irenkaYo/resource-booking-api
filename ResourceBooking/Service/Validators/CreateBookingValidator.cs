using FluentValidation;
using Infrastructure.DTO;

namespace Service.Validators;

public class CreateBookingValidator : AbstractValidator<CreateBookingDto>
{
    public CreateBookingValidator()
    {
        RuleFor(x => x.StartTime)
            .NotEmpty();
        
        RuleFor(x => x.EndTime)
            .NotEmpty();

        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime)
            .WithMessage("EndTime must be greater than StartTime");
    }
}