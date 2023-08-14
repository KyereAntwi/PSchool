using FluentValidation;

namespace PSchool.Application.Features.Parents.Commands.CreateParent;

public class CreateParentCommandValidator : AbstractValidator<CreateParentCommand>
{
    public CreateParentCommandValidator()
    {
        RuleFor(a => a.FirstName)
            .NotEmpty().WithMessage("{PropertyName} should not be empty")
            .NotNull();

        RuleFor(a => a.LastName)
            .NotEmpty().WithMessage("{PropertyName} should not be empty")
            .NotNull();

        RuleFor(a => a.Email)
            .NotEmpty().WithMessage("{PropertyName} should not be empty")
            .NotNull();
    }
}
