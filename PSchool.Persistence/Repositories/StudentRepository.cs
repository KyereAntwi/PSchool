using PSchool.Application.Contracts;
using PSchool.Domain.Entities;

namespace PSchool.Persistence.Repositories;

public class StudentRepository : BaseRepository<Student>, IStudentRepository
{
    public Task<IReadOnlyList<Student>> GetStudentsByParentId(Guid parentId)
    {
        throw new NotImplementedException();
    }
}
