using System.ComponentModel.DataAnnotations;

using Cinema.Models;
namespace Cinema.DTO;

public class UpdateShowTimeRequest
{
    [Required(ErrorMessage = "ShowDateTime is required.")]
    public DateTime ShowDateTime { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "MovieId must be a positive number.")]
    public int MovieId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "AuditoriumId must be a positive number.")]
    public int AuditoriumId { get; set; }
}
