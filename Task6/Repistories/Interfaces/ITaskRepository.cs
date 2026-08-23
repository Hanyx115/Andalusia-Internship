using Task6.Models;
namespace TaskManager.Api.Repositories.Interfaces;

public interface ITasksRepository
{
    Task<TaskItem> CreateTask(TaskItem newTask);
    Task<TaskItem?> GetTaskById(int id);
    Task<IEnumerable<TaskItem>> GetAllTasks();
    Task UpdateTask(TaskItem task);
    Task DeleteTask(int id);
    Task<IEnumerable<TaskItem>> GetTasksByUser(int userId);
}