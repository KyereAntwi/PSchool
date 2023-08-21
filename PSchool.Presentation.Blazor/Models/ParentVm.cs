using System.ComponentModel.DataAnnotations;

namespace PSchool.Presentation.Blazor.Models;

public class ParentVm
{
    public Guid Id { get; set; }
    [Required]
    public string LastName { get; set; } = string.Empty;
    [Required]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public string? Username { get; set; }

    public override string ToString()
    {
        return $"{FirstName} {LastName}";
    }
}
