using System.ComponentModel.DataAnnotations;
namespace TaskManagerApi.DTOs;
public class CreateTaskRequest
{
    
    public string Title { get; set; } = string.Empty;
    [StringLength(2000)]
    public string? Description { get; set; }

    public DateTime? DueDate { get; set; }

}
