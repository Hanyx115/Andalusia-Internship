using Microsoft.EntityFrameworkCore;
using Task6.Data;
using Task6.Models;

using TaskManager.Api.Repositories.Interfaces;

namespace TaskManager.Api.Repositories;

public class TasksRepository : ITasksRepository
{
    private readonly ApplicationDbContext _dbcontext;

    public TasksRepository(ApplicationDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task<TaskItem> CreateTask(TaskItem newTask)
    {
        _dbcontext.TaskItems.Add(newTask);
        await _dbcontext.SaveChangesAsync();
        return newTask;
    }

    public async Task<TaskItem?> GetTaskById(int id)
    {
        return await _dbcontext.TaskItems
            .Include(t => t.User)   // eager load related user
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<TaskItem>> GetAllTasks()
    {
        return await _dbcontext.TaskItems
            .Include(t => t.User)
            .ToListAsync();
    }

    public async Task UpdateTask(TaskItem task)
    {
        _dbcontext.TaskItems.Update(task);
        await _dbcontext.SaveChangesAsync();
    }

    public async Task DeleteTask(int id)
    {
        var task = await _dbcontext.TaskItems.FindAsync(id);
        if (task != null)
        {
            _dbcontext.TaskItems.Remove(task);
            await _dbcontext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<TaskItem>> GetTasksByUser(int userId)
    {
        return await _dbcontext.TaskItems
            .Where(t => t.UserId == userId)
            .Include(t => t.User)
            .ToListAsync();
    }
}