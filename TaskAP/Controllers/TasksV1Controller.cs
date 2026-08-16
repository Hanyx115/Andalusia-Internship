using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using TaskAP.Repo;

[ApiController]
[ApiVersion(1.0, Deprecated = true)]
[Route("api/v{version:apiVersion}/tasks")]
public class TasksV1Controller : ControllerBase
{
    private readonly IProductRepo _repository;

    public TasksV1Controller(IProductRepo repository)
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
            isCompleted = task.Price
        });

        return Ok(result);
    }
}