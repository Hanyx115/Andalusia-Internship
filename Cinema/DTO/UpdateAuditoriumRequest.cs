using System.ComponentModel.DataAnnotations;

using Cinema.Models;
namespace Cinema.DTO;

public class UpdateAuditoriumRequest
{
    [Range(1, int.MaxValue, ErrorMessage = "RoomNumber must be a positive number.")]
    public int RoomNumber { get; set; }

    [Range(1, 1000, ErrorMessage = "Capacity must be between 1 and 1000.")]
    public int Capacity { get; set; }

    public bool Available { get; set; }
}
