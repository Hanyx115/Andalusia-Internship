using Task6.Models;
using TaskManager.Api.Repositories.Interfaces;
using TaskManager.Api.Services.Interfaces;

namespace Task6.Api.Services;

public class TasksService : ITasksService
{
    private readonly ITasksRepository _taskRepo;

    public TasksService(ITasksRepository taskRepo)
    {
        _taskRepo = taskRepo;
    }

    public async Task<TaskItem> CreateTask(TaskItem newTask)
    {
        // Optionally add business validation here
        return await _taskRepo.CreateTask(newTask);
    }

    public async Task<TaskItem?> GetTaskById(int id)
    {
        return await _taskRepo.GetTaskById(id);
    }

    public async Task<IEnumerable<TaskItem>> GetAllTasks()
    {
        return await _taskRepo.GetAllTasks();
    }

    public async Task UpdateTask(TaskItem task)
    {
        await _taskRepo.UpdateTask(task);
    }

    public async Task DeleteTask(int id)
    {
        await _taskRepo.DeleteTask(id);
    }

    public async Task<IEnumerable<TaskItem>> GetTasksByUser(int userId)
    {
        return await _taskRepo.GetTasksByUser(userId);
    }
}