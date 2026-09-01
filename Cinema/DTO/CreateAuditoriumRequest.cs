using System.ComponentModel.DataAnnotations;

namespace Cinema.DTO;

public class CreateAuditoriumRequest
{
    // hall range from 1 to biggest int
    [Range(1, int.MaxValue, ErrorMessage = "RoomNumber must be a positive number.")]
    public int RoomNumber { get; set; }

// capacitiy from 1 to 1000
    [Range(1, 1000, ErrorMessage = "Capacity must be between 1 and 1000.")]
    public int Capacity { get; set; }

    public bool Available { get; set; }
}
