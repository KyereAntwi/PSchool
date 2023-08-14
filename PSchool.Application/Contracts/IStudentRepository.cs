using PSchool.Domain.Entities;

namespace PSchool.Application.Contracts;

public interface IStudentRepository : IAsyncRepository<Student>
{
    Task<IReadOnlyList<Student>> GetStudentsByParentId(Guid parentId);
}

