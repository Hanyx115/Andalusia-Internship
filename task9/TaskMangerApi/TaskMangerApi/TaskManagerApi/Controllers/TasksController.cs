using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TaskManagerApi.DTOs;
using TaskManagerApi.Services;
namespace TaskManagerApi.Controllers;
[ApiController]
[Authorize]
[Route("api/tasks")]
public class TasksController(ITaskService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedResult<TaskSummaryDto>>> GetAll([FromQuery] TaskQuery query, CancellationToken ct) =>
        Ok(await service.GetAllAsync(query, ct));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TaskItemDto>> GetById(int id, CancellationToken ct)
    {
        var task = await service.GetByIdAsync(id, ct);
        return task is null ? NotFound() : Ok(task);
    }
    [HttpPost]
    public async Task<ActionResult<TaskItemDto>> Create(CreateTaskRequest request, CancellationToken ct)
    {
        var task = await service.CreateAsync(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }
    [HttpPut("{id:int}")]
    public async Task<ActionResult<TaskItemDto>> Update(int id, UpdateTaskRequest request, CancellationToken ct)
    {
        var task = await service.UpdateAsync(id, request, ct);
        return task is null ? NotFound() : Ok(task);
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct) =>
        await service.DeleteAsync(id, ct) ? NoContent() : NotFound();
}
