using FluentValidation;

namespace PSchool.Application.Features.Parents.Commands.UpdateParent;

public class UpdateParentCommandValidator : AbstractValidator<UpdateParentCommand>
{
    public UpdateParentCommandValidator()
    {
        RuleFor(a => a.Id)
            .NotEmpty().WithMessage("{PropertyName} should not be empty")
            .NotNull();

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
