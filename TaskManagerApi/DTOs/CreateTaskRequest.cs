using System.ComponentModel.DataAnnotations;
namespace TaskManagerApi.DTOs;
public class CreateTaskRequest
{
    [Required, StringLength(200)]
    public string Title { get; set; } = string.Empty;
    [StringLength(2000)]
    public string? Description { get; set; }

}
