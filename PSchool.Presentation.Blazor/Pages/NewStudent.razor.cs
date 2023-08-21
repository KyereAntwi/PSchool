using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using PSchool.Presentation.Blazor.Models;
using PSchool.Presentation.Blazor.Services;

namespace PSchool.Presentation.Blazor.Pages;

public partial class NewStudent
{
    [Inject] public ICoreDataService CoreDataService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private ISnackbar SnackBar { get; set; } = default!;

    [Parameter]
    public string? studentId { get; set; }
    [Parameter]
    public string? parentId { get; set; }

    public StudentVm? Student { get; set; } = new StudentVm();

    string? Title = $"Register a new student";

    protected override async Task OnInitializedAsync()
    {
        if (!string.IsNullOrEmpty(studentId))
        {
            var student = await CoreDataService.GetStudentVmAsync(new Guid(studentId));

            if (student is not null)
            {
                Student = student;
                Title = $"Update {student.FirstName}";
                parentId = student.ParentId.ToString();
            }
            else
                SnackBar.Add($"There was a problem fetching the specified student with id {studentId}", severity: Severity.Error);
        }
    }

    public async Task OnValidSubmit(EditContext context)
    {
        string message;

        if (!string.IsNullOrEmpty(studentId) && Student is not null)
        {
            await CoreDataService.UpdateStudentAsync(Student);
            message = "Changes made successfully";
        }
        else
        {
            if (Student is not null && !string.IsNullOrEmpty(parentId))
            {
                Student.ParentId = new Guid(parentId);
                await CoreDataService.CreateStudentAsync(Student);
            }

            message = "New student registered successfully";
        }

        SnackBar.Add($"{message}", severity: Severity.Success);
        NavigationManager.NavigateTo("/students");
    }
}
