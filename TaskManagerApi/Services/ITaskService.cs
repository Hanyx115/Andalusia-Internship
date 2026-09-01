using TaskManagerApi.DTOs;
namespace TaskManagerApi.Services;
public interface ITaskService
{
    Task<PagedResult<TaskSummaryDto>> GetAllAsync(TaskQuery query, CancellationToken ct);
    Task<TaskItemDto?> GetByIdAsync(int id, CancellationToken ct);
    Task<TaskItemDto> CreateAsync(CreateTaskRequest request, CancellationToken ct);
    Task<TaskItemDto?> UpdateAsync(int id, UpdateTaskRequest request, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}
