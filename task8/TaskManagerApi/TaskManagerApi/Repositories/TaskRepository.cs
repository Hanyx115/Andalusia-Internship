using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.Models;
namespace TaskManagerApi.Repositories;
public class TaskRepository(AppDbContext context) : ITaskRepository
{
    public async Task<(IReadOnlyList<TaskItem> Items, int TotalCount)> GetAllAsync(
        string? search, bool? isCompleted, int page, int pageSize, CancellationToken ct)
    {
        var query = context.Tasks.AsNoTracking().AsQueryable();
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
    public async Task<TaskItem?> GetByIdAsync(int id, CancellationToken ct) =>
        await context.Tasks.SingleOrDefaultAsync(t => t.Id == id, ct);
    public async Task AddAsync(TaskItem task, CancellationToken ct)
    {
        await context.Tasks.AddAsync(task, ct);
        await context.SaveChangesAsync(ct);
    }
    public async Task UpdateAsync(TaskItem task, CancellationToken ct)
    {
        context.Tasks.Update(task);
        await context.SaveChangesAsync(ct);
    }
    public async Task DeleteAsync(TaskItem task, CancellationToken ct)
    {
        context.Tasks.Remove(task);
        await context.SaveChangesAsync(ct);
    }
}
