using Microsoft.AspNetCore.Components;
using MudBlazor;
using PSchool.Presentation.Blazor.Models;
using PSchool.Presentation.Blazor.Services;

namespace PSchool.Presentation.Blazor.Pages;

public partial class StudentsListPage
{
    [Inject] private ICoreDataService CoreDataService { get; set; } = default!;
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private ISnackbar SnackBar { get; set; } = default!;
    [Inject] private IDialogService DialogService { get; set; } = default!;

    public IEnumerable<StudentVm> StudentList { get; set; } = new List<StudentVm>();
    bool loading = true;

    protected override async Task OnInitializedAsync()
    {
        await fetchStudentList(20, 1);
    }

    async Task fetchStudentList(int size, int page)
    {
        loading = true;

        var paggedList = await CoreDataService.GetStudentVmsAsync(size, page);
        StudentList = paggedList.ListItems;
        loading = false;

        loading = false;
    }

    async Task filterStudentsListByParent(Guid parentId)
    {
        loading = true;
        var studentist = await CoreDataService.GetStudentVmsByParentAsync(parentId);

        if (studentist?.Count() > 0)
        {
            StudentList = studentist;
        }
        else
        {
            SnackBar.Add("No students found for specified parent", Severity.Info);
            await fetchStudentList(20, 1);
        }

        loading = false;
        StateHasChanged();
    }

    async Task onDeleteParent(Guid studentId)
    {
        string state;

        bool? result = await DialogService.ShowMessageBox(
            "Warning",
            "Deleting student can not be undone!",
            yesText: "Delete!", cancelText: "Cancel");

        state = result == null ? "Canceled" : "Deleted!";

        if (state == "Deleted!")
        {
            await CoreDataService.DeleteStudentAsync(studentId);
            StudentList = StudentList.Where(s => s.Id != studentId);
            SnackBar.Add("Student item was deleted successfully", Severity.Success);
        }
        else
        {
            SnackBar.Add("Delete action was cancelled", Severity.Normal);
        }

        StateHasChanged();
    }

    void NavigateToParentPage(Guid parentId)
    {
        NavigationManager.NavigateTo($"/parents/{parentId}");
    }

    void UpdateStudentDetials(Guid studentId)
    {
        NavigationManager.NavigateTo($"/UpdateStudent/{studentId}");
    }
}
