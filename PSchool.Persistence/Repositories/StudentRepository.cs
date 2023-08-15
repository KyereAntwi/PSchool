using Microsoft.EntityFrameworkCore;
using PSchool.Application.Contracts;
using PSchool.Domain.Entities;
using PSchool.Persistence.Data;

namespace PSchool.Persistence.Repositories;

public class StudentRepository : BaseRepository<Student>, IStudentRepository
{
    private readonly SchoolDbContext _dbContext;

    public StudentRepository(SchoolDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Student>> GetStudentsByParentId(Guid parentId)
    {
        return await _dbContext.Students.Where(s => s.ParentId == parentId).ToListAsync();
    }
}
