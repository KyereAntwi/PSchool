
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using PSchool.Presentation.Blazor.Models;
using PSchool.Presentation.Blazor.Services;

namespace PSchool.Presentation.Blazor.Pages;

public partial class NewParentPage
{
    [Inject] public ICoreDataService CoreDataService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private ISnackbar SnackBar { get; set; } = default!;

    [Parameter]
    public string? parentId { get; set; }

    public ParentVm? Parent { get; set; } = new ParentVm();

    string? Title = "Add a new parent";

    protected override async Task OnInitializedAsync()
    {
        if(!string.IsNullOrEmpty(parentId))
        {
            var parent = await CoreDataService.GetParentVmAsync(new Guid(parentId));

            if (parent is not null)
            {
                Parent = parent;
                Title = $"Update {parent.FirstName}";
            }
            else
                SnackBar.Add($"There was a problem fetching the specified parent with id {parentId}", severity: Severity.Error);
        }
    }

    public async Task OnValidSubmit(EditContext context)
    {
        string message;

        if (!string.IsNullOrEmpty(parentId) && Parent is not null)
        {
            await CoreDataService.UpdateParentAsync(Parent);
            message = "Changes made successfully";
        }
        else
        {
            if(Parent is not null)
                await CoreDataService.CreateAParentAsync(Parent);
            message = "New parent created successfully";
        }

        SnackBar.Add($"{message}", severity: Severity.Success);
        NavigationManager.NavigateTo("/");
    }
}
