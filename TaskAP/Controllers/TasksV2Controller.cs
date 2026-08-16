using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using TaskAP.Repo;

[ApiController]
[ApiVersion(2.0)]
[Route("api/v{version:apiVersion}/tasks")]
public class TasksV2Controller : ControllerBase
{
    private readonly IProductRepo _repository;

    public TasksV2Controller(IProductRepo repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult GetTasks()
    {
        var tasks = _repository.GetAll();

        var result = tasks.Select(task => new
        {
            id = task.Id,
            title = task.Name,

            status = task.IsCompleted
                ? "completed"
                : "pending",

            dueDate = task.DueDate,
            createdAt = task.CreatedAt
        });

        return Ok(result);
    }
}