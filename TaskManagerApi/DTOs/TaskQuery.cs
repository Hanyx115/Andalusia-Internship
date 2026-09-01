using System.ComponentModel.DataAnnotations;
namespace TaskManagerApi.DTOs;
public class TaskQuery
{
    [StringLength(200)] public string? Search { get; set; }
    public bool? IsCompleted { get; set; }
    [Range(1, 1000000)] public int Page { get; set; } = 1;
    [Range(1, 100)] public int PageSize { get; set; } = 10;
}
