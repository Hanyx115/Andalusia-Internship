using System.ComponentModel.DataAnnotations;
namespace TaskManagerApi.DTOs;
public class UpdateTaskRequest
{
    [Required, StringLength(200)]
    public string Title { get; set; } = string.Empty;
    [StringLength(2000)]
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
}
