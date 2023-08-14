using FluentValidation;

namespace PSchool.Application.Features.Students.Commands.CreateStudent;

public class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
{
    public CreateStudentCommandValidator()
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
