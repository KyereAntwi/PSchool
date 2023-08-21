using System.ComponentModel.DataAnnotations;

namespace PSchool.Presentation.Blazor.Models
{
	public class StudentVm
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

        [Required]
        public Guid ParentId { get; set; }
    }
}

