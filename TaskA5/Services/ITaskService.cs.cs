using TaskA5.Model;

namespace TaskA5.Services
{
    public interface ITaskService
    {
        PagedResult<Task> GetAll(TaskFilterParams parameters);
    }
}