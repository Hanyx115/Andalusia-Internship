using Cinema.Models;
namespace Cinema.DTO;

public class MovieV1Dto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool AvailableInCinema { get; set; }
}
