using Microsoft.AspNetCore.Mvc;
using Task6.Models;
using TaskManager.Api.Services.Interfaces;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITasksService _taskService;

    public TasksController(ITasksService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks()
    {
        var tasks = await _taskService.GetAllTasks();
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskItem>> GetTask(int id)
    {
        var task = await _taskService.GetTaskById(id);
        if (task == null)
            return NotFound();
        return Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<TaskItem>> CreateTask(TaskItem task)
    {
        var created = await _taskService.CreateTask(task);
        return CreatedAtAction(nameof(GetTask), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, TaskItem task)
    {
        if (id != task.Id)
            return BadRequest();

        await _taskService.UpdateTask(task);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        await _taskService.DeleteTask(id);
        return NoContent();
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasksByUser(int userId)
    {
        var tasks = await _taskService.GetTasksByUser(userId);
        return Ok(tasks);
    }
}