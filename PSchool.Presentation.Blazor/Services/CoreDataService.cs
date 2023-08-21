using System.Net.Mime;
using System.Text;
using System.Text.Json;
using MudBlazor;
using PSchool.Presentation.Blazor.Models;

namespace PSchool.Presentation.Blazor.Services;

public class CoreDataService : ICoreDataService
{
    private readonly HttpClient _httpClient;

    public CoreDataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task CreateAParentAsync(ParentVm parentVm)
    {
        var model = new StringContent(
            JsonSerializer.Serialize(parentVm),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);

        await _httpClient.PostAsync("api/parents", model);
    }

    public async Task CreateStudentAsync(StudentVm studentVm)
    {
        var model = new StringContent(
            JsonSerializer.Serialize(studentVm),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);

        await _httpClient.PostAsync("api/students", model);
    }

    public async Task DeleteParentAsync(Guid parentId)
    {
        await _httpClient.DeleteAsync($"api/parents/{parentId}");
    }

    public async Task DeleteStudentAsync(Guid studentId)
    {
        await _httpClient.DeleteAsync($"api/students/{studentId}");
    }

    public async Task<IEnumerable<ParentVm>?> GetAllParentAsync()
    {
        var parentList = await JsonSerializer.DeserializeAsync<IEnumerable<ParentVm>>(
            await _httpClient.GetStreamAsync($"api/parents/all"),
        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        return parentList;
    }

    public async Task<ParentVm?> GetParentVmAsync(Guid parentId)
    {
        return await JsonSerializer.DeserializeAsync<ParentVm>(await _httpClient.GetStreamAsync($"api/parents/{parentId}"),
        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
    }

    public async Task<PaggedList<ParentVm>?> GetParentVmsAsync(int pageSize = 20, int page = 1)
    {
        var parentList = await JsonSerializer.DeserializeAsync<PaggedList<ParentVm>>(
            await _httpClient.GetStreamAsync($"api/parents?pageSize={pageSize}&page={page}"),
        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        return parentList;
    }

    public async Task<StudentVm?> GetStudentVmAsync(Guid studentId)
    {
        return await JsonSerializer.DeserializeAsync<StudentVm>(await _httpClient.GetStreamAsync($"api/students/{studentId}"),
        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
    }

    public async Task<PaggedList<StudentVm>?> GetStudentVmsAsync(int pageSize, int page)
    {
        var studentList = await JsonSerializer.DeserializeAsync<PaggedList<StudentVm>>(
            await _httpClient.GetStreamAsync($"api/students?pageSize={pageSize}&page={page}"),
        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        return studentList;
    }

    public async Task<IEnumerable<StudentVm>?> GetStudentVmsByParentAsync(Guid parentId)
    {
        return await JsonSerializer.DeserializeAsync<IEnumerable<StudentVm>>(await _httpClient.GetStreamAsync($"api/students/byparent/{parentId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
    }

    public async Task UpdateParentAsync(ParentVm parentVm)
    {
        var model = new StringContent(
            JsonSerializer.Serialize(parentVm),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);

        await _httpClient.PutAsync("api/parents", model);
    }

    public async Task UpdateStudentAsync(StudentVm studentVm)
    {
        var model = new StringContent(
            JsonSerializer.Serialize(studentVm),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);

        await _httpClient.PutAsync("api/students", model);
    }
}
