namespace PSchool.Domain.Entities;

public class Parent : Person
{
    public ICollection<Student> Students { get; set; } = default!;
}

