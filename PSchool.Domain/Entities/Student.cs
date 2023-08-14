namespace PSchool.Domain.Entities;

public class Student : Person
{
    public Guid ParentId { get; set; }
    public Parent? Parent { get; set; }
}

