using Microsoft.AspNetCore.Components;
using PSchool.Presentation.Blazor.Models;
using PSchool.Presentation.Blazor.Services;

namespace PSchool.Presentation.Blazor.Components;

public partial class StudentFilterByParentForm
{
    [Inject] private ICoreDataService CoreDataService { get; set; } = default!;

    [Parameter]
    public EventCallback<Guid> OnParentIdChanged { get; set; }

    public IEnumerable<ParentVm>? ParentsList { get; set; } = new List<ParentVm>();
    public Guid SelectedId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var parentsList = await CoreDataService.GetAllParentAsync();
        ParentsList = parentsList;
    }

    async Task ParentIdChanged()
    {
        if (Guid.Empty != SelectedId)
        {
            await OnParentIdChanged.InvokeAsync(SelectedId);
        }
    }
}

