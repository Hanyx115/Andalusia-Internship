using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.Models;
namespace TaskManagerApi.Repositories;
public class TaskRepository(AppDbContext context, TaskManagerApi.Services.ICurrentUser currentUser) : ITaskRepository
{
    public async Task<(IReadOnlyList<TaskItem> Items, int TotalCount)> GetAllAsync(
        int userId, string? search, bool? isCompleted, int page, int pageSize, CancellationToken ct)
    {
        if (userId != currentUser.UserId) throw new UnauthorizedAccessException();
        var query = context.Tasks.AsNoTracking().Where(t => t.UserId == userId);
        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            // Title's CI collation makes SQL Server matching case-insensitive.
            query = query.Where(t => t.Title.Contains(term));
        }
        if (isCompleted.HasValue)
            query = query.Where(t => t.IsCompleted == isCompleted.Value);
        var count = await query.CountAsync(ct);
        var items = await query.OrderBy(t => t.Id)
            .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(ct);
        return (items, count);
    }
    public async Task<TaskItem?> GetByIdAsync(int id, int userId, CancellationToken ct)
    {
        if (userId != currentUser.UserId) throw new UnauthorizedAccessException();
        return await context.Tasks.SingleOrDefaultAsync(t => t.Id == id && t.UserId == userId, ct);
    }
    public async Task AddAsync(TaskItem task, CancellationToken ct)
    {
        EnsureOwner(task);
        await context.Tasks.AddAsync(task, ct);
        await context.SaveChangesAsync(ct);
    }
    public async Task<bool> UpdateAsync(TaskItem task, CancellationToken ct)
    {
        EnsureOwner(task);
        var userId = currentUser.UserId;
        var affected = await context.Tasks.Where(t => t.Id == task.Id && t.UserId == userId)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(t => t.Title, task.Title)
                .SetProperty(t => t.Description, task.Description)
                .SetProperty(t => t.IsCompleted, task.IsCompleted)
                .SetProperty(t => t.DueDate, task.DueDate)
                .SetProperty(t => t.UpdatedAt, task.UpdatedAt), ct);
        return affected == 1;
    }
    public async Task<bool> DeleteAsync(TaskItem task, CancellationToken ct)
    {
        EnsureOwner(task);
        var userId = currentUser.UserId;
        return await context.Tasks.Where(t => t.Id == task.Id && t.UserId == userId)
            .ExecuteDeleteAsync(ct) == 1;
    }
    private void EnsureOwner(TaskItem task)
    {
        if (task.UserId != currentUser.UserId) throw new UnauthorizedAccessException();
    }
}
