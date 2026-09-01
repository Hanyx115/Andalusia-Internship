using Cinema.Models;
namespace Cinema.DTO;

public class ShowTimeDto
{
    public int Id { get; set; }
    public DateTime ShowDateTime { get; set; }

    public int MovieId { get; set; }
    public string MovieName { get; set; } = string.Empty;

    public int AuditoriumId { get; set; }
    public int RoomNumber { get; set; }
}
