using TaskManagerApi.Models;
namespace TaskManagerApi.Repositories;
public interface ITaskRepository
{
    Task<(IReadOnlyList<TaskItem> Items, int TotalCount)> GetAllAsync(int userId, string? search, bool? isCompleted, int page, int pageSize, CancellationToken ct);
    Task<TaskItem?> GetByIdAsync(int id, int userId, CancellationToken ct);
    Task AddAsync(TaskItem task, CancellationToken ct);
    Task<bool> UpdateAsync(TaskItem task, CancellationToken ct);
    Task<bool> DeleteAsync(TaskItem task, CancellationToken ct);
}
