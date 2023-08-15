using PSchool.Application.Contracts;
using PSchool.Domain.Entities;
using PSchool.Persistence.Data;

namespace PSchool.Persistence.Repositories;

public class ParentRepository : BaseRepository<Parent>, IParentRepository
{
    public ParentRepository(SchoolDbContext dbContext) : base(dbContext)
    {
    }
}
