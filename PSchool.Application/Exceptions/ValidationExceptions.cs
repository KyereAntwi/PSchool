using FluentValidation.Results;

namespace PSchool.Application.Exceptions;

public class ValidationExceptions : Exception
{
    public List<string> ValdationErrors { get; set; }

    public ValidationExceptions(ValidationResult validationResult)
    {
        ValdationErrors = new List<string>();

        foreach (var validationError in validationResult.Errors)
        {
            ValdationErrors.Add(validationError.ErrorMessage);
        }
    }
}
