using Microsoft.AspNetCore.Components;
using MudBlazor;
using PSchool.Presentation.Blazor.Models;
using PSchool.Presentation.Blazor.Services;

namespace PSchool.Presentation.Blazor.Pages;

public partial class ParentsListPage
{
    [Inject] private ICoreDataService CoreDataService { get; set; } = default!;
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private ISnackbar SnackBar { get; set; } = default!;
    [Inject] private IDialogService DialogService { get; set; } = default!;

    public IEnumerable<ParentVm> ParentsList { get; set; } = new List<ParentVm>();
    bool loading = true;

    protected override async Task OnInitializedAsync()
    {
        await fetchParentListAsync(20, 1);
    }

    async Task fetchParentListAsync(int size, int page)
    {
        loading = true;
        var paggedList = await CoreDataService.GetParentVmsAsync(size, page);
        ParentsList = paggedList.ListItems;
        loading = false;
    }

    async Task onDeleteParent(Guid parentId)
    {
        string state;

        bool? result = await DialogService.ShowMessageBox(
            "Warning",
            "Deleting parent can not be undone!",
            yesText: "Delete!", cancelText: "Cancel");

        state = result == null ? "Canceled" : "Deleted!";

        if (state == "Deleted!")
        {
            await CoreDataService.DeleteParentAsync(parentId);
            ParentsList = ParentsList.Where(p => p.Id != parentId);
            SnackBar.Add("Parent item was deleted successfully", Severity.Info);
        }
        else
        {
            SnackBar.Add("Delete action was cancelled", Severity.Normal);
        }

        StateHasChanged();

    }

    void UpdateParentDetails(Guid parentId)
    {
        NavigationManager.NavigateTo($"/New-Parent/{parentId}");
    }

    void RegisterStudentForParent(Guid parentId)
    {
        NavigationManager.NavigateTo($"/NewStudent/{parentId}");
    }

    void ViewDetailPage(Guid parentId)
    {
        NavigationManager.NavigateTo($"/parents/{parentId}");
    }
}
