using Microsoft.EntityFrameworkCore;
using PSchool.Domain.Entities;

namespace PSchool.Persistence.Data;

public class SchoolDbContext : DbContext
{
    public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
    {
    }

    public DbSet<Parent> Parents { get; set; }
    public DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SchoolDbContext).Assembly);

        modelBuilder.Entity<Parent>().HasData(new Parent()
        {
            Id = new Guid("9245fe4a-d402-451c-b9ed-9c1a04247482"),
            FirstName = "John",
            LastName = "Doe",
            Username = "john.doe",
            Email = "john.doe@email.com"
        });

        modelBuilder.Entity<Parent>().HasData(new Parent()
        {
            Id = new Guid("8245fe4a-d402-451c-b9ed-9c1a04247482"),
            FirstName = "William",
            LastName = "Smith",
            Username = "william.smith",
            Email = "william.smith@email.com"
        });

        modelBuilder.Entity<Student>().HasData(new Student()
        {
            Id = new Guid("7245fe4a-d402-451c-b9ed-9c1a04247482"),
            FirstName = "Chalese",
            LastName = "Doe",
            Username = "chalese.doe",
            Email = "chalese.doe@email.com",
            ParentId = new Guid("9245fe4a-d402-451c-b9ed-9c1a04247482")
        });

        modelBuilder.Entity<Student>().HasData(new Student()
        {
            Id = new Guid("6245fe4a-d402-451c-b9ed-9c1a04247482"),
            FirstName = "Joyce",
            LastName = "Doe",
            Username = "joyce.doe",
            Email = "joyce.doe@email.com",
            ParentId = new Guid("9245fe4a-d402-451c-b9ed-9c1a04247482")
        });

        modelBuilder.Entity<Student>().HasData(new Student()
        {
            Id = new Guid("5245fe4a-d402-451c-b9ed-9c1a04247482"),
            FirstName = "Fredrick",
            LastName = "Smith",
            Username = "fredrick.smith",
            Email = "fredrick.smith@email.com",
            ParentId = new Guid("8245fe4a-d402-451c-b9ed-9c1a04247482")
        });
    }
}
