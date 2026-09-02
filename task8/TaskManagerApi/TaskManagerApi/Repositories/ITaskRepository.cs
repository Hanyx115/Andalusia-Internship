using TaskManagerApi.Models;
namespace TaskManagerApi.Repositories;
public interface ITaskRepository
{
    Task<(IReadOnlyList<TaskItem> Items, int TotalCount)> GetAllAsync(string? search, bool? isCompleted, int page, int pageSize, CancellationToken ct);
    Task<TaskItem?> GetByIdAsync(int id, CancellationToken ct);
    Task AddAsync(TaskItem task, CancellationToken ct);
    Task UpdateAsync(TaskItem task, CancellationToken ct);
    Task DeleteAsync(TaskItem task, CancellationToken ct);
}
