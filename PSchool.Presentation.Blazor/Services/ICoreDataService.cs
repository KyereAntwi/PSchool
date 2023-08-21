using PSchool.Presentation.Blazor.Models;

namespace PSchool.Presentation.Blazor.Services;

public interface ICoreDataService
{
    #region Parents services
    Task<PaggedList<ParentVm>?> GetParentVmsAsync(int pageSize, int page);
    Task<IEnumerable<ParentVm>?> GetAllParentAsync();
    Task<ParentVm?> GetParentVmAsync(Guid parentId);
    Task DeleteParentAsync(Guid parentId);
    Task UpdateParentAsync(ParentVm parentVm);
    Task CreateAParentAsync(ParentVm parentVm);
    #endregion

    #region Students services
    Task<PaggedList<StudentVm>?> GetStudentVmsAsync(int pageSize, int page);
    Task<StudentVm?> GetStudentVmAsync(Guid studentId);
    Task<IEnumerable<StudentVm>?> GetStudentVmsByParentAsync(Guid parentId);
    Task DeleteStudentAsync(Guid studentId);
    Task UpdateStudentAsync(StudentVm studentVm);
    Task CreateStudentAsync(StudentVm studentVm);
    #endregion
}
