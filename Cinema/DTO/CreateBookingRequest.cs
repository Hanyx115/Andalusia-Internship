using System.ComponentModel.DataAnnotations;

using Cinema.Models;
namespace Cinema.DTO;

public class CreateBookingRequest
{
    [Range(1, int.MaxValue, ErrorMessage = "CustomerId must be a positive number.")]
    public int CustomerId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "ShowTimeId must be a positive number.")]
    public int ShowTimeId { get; set; }
}
