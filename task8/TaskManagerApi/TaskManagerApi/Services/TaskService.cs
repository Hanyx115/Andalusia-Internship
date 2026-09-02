using AutoMapper;
using TaskManagerApi.DTOs;
using TaskManagerApi.Models;
using TaskManagerApi.Repositories;
namespace TaskManagerApi.Services;
public class TaskService(ITaskRepository repository, IMapper mapper) : ITaskService
{
    public async Task<PagedResult<TaskSummaryDto>> GetAllAsync(TaskQuery query, CancellationToken ct)
    {
        var (items, count) = await repository.GetAllAsync(
            query.Search, query.IsCompleted, query.Page, query.PageSize, ct);
        return new(mapper.Map<List<TaskSummaryDto>>(items), count, query.Page, query.PageSize);
    }
    public async Task<TaskItemDto?> GetByIdAsync(int id, CancellationToken ct)
    {
        var task = await repository.GetByIdAsync(id, ct);
        return task is null ? null : mapper.Map<TaskItemDto>(task);
    }
    public async Task<TaskItemDto> CreateAsync(CreateTaskRequest request, CancellationToken ct)
    {
        var task = mapper.Map<TaskItem>(request);
        await repository.AddAsync(task, ct);
        return mapper.Map<TaskItemDto>(task);
    }
    public async Task<TaskItemDto?> UpdateAsync(int id, UpdateTaskRequest request, CancellationToken ct)
    {
        var task = await repository.GetByIdAsync(id, ct);
        if (task is null) return null;
        mapper.Map(request, task);
        task.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(task, ct);
        return mapper.Map<TaskItemDto>(task);
    }
    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        var task = await repository.GetByIdAsync(id, ct);
        if (task is null) return false;
        await repository.DeleteAsync(task, ct);
        return true;
    }
}
