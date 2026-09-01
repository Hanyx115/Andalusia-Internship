using System.ComponentModel.DataAnnotations;

using Cinema.Models;
namespace Cinema.DTO;

public class UpdateMovieRequest
{
    [Required(ErrorMessage = "Name is required.")]
    [MaxLength(200, ErrorMessage = "Name cannot exceed 200 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Genre is required.")]
    [MaxLength(100, ErrorMessage = "Genre cannot exceed 100 characters.")]
    public string Genre { get; set; } = string.Empty;

    [Required(ErrorMessage = "ReleaseDate is required.")]
    public DateTime ReleaseDate { get; set; }

    public bool AvailableInCinema { get; set; }
}
