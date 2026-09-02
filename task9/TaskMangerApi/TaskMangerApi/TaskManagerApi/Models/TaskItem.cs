namespace TaskManagerApi.Models;
public class TaskItem
{
    public int Id { get; set; }
    // Nullable only to preserve pre-authentication rows; new API tasks always get an owner.
    public int? UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public DateTime? DueDate { get; set; }
}
