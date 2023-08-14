namespace PSchool.Application.Features.Common;

public class StudentDto
{
    public Guid Id { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Username { get; set; }
}
