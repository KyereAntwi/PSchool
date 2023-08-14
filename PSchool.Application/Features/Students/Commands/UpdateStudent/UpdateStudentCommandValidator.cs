using FluentValidation;

namespace PSchool.Application.Features.Students.Commands.UpdateStudent;

public class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentCommand>
{
    public UpdateStudentCommandValidator()
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
