using Microsoft.AspNetCore.Components;
using MudBlazor;
using PSchool.Presentation.Blazor.Models;
using PSchool.Presentation.Blazor.Services;

namespace PSchool.Presentation.Blazor.Pages;

public partial class ParentDetailsPage
{
	[Inject] public ICoreDataService CoreDataService { get; set; } = default!;
	[Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private ISnackbar SnackBar { get; set; } = default!;
    [Inject] private IDialogService DialogService { get; set; } = default!;

    [Parameter] public string? parentId { get; set; }

	public ParentVm? Parent { get; set; }
	public IEnumerable<StudentVm>? Students { get; set; } = new List<StudentVm>();

	bool loading = true;

	string Fullname = "Parent detail page";

    protected override async Task OnInitializedAsync()
    {
		await fetchParentDetailAsync();
		await fetchParentStudentsAsync();

		loading = false;
    }

	async Task fetchParentDetailAsync()
	{
		if (!string.IsNullOrEmpty(parentId))
		{
			Parent = await CoreDataService.GetParentVmAsync(new Guid(parentId));
			Fullname = $"{Parent.FirstName} {Parent.LastName}";
		}
	}

	async Task fetchParentStudentsAsync()
	{
        if (!string.IsNullOrEmpty(parentId))
        {
            Students = await CoreDataService.GetStudentVmsByParentAsync(new Guid(parentId));
        }
    }

	void RegisterStudent()
	{
		NavigationManager.NavigateTo($"/NewStudent/{Parent?.Id}");
	}

    async Task DeleteParent()
	{
        string state;

        bool? result = await DialogService.ShowMessageBox(
            "Warning",
            "Deleting parent can not be undone!",
            yesText: "Delete!", cancelText: "Cancel");

        state = result == null ? "Canceled" : "Deleted!";

        if (state == "Deleted!")
        {
            await CoreDataService.DeleteParentAsync(Parent.Id);
            SnackBar.Add("Parent item was deleted successfully", Severity.Info);
            NavigationManager.NavigateTo("/");
        }
        else
        {
            SnackBar.Add("Delete action was cancelled", Severity.Normal);
        }
    }
}

