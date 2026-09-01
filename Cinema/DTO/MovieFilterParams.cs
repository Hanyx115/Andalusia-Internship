using Cinema.Models;
namespace Cinema.DTO;

public class MovieFilterParams : PaginationParams
{
    public string? Search { get; set; }

    public string? Genre { get; set; }

    public string? SortBy { get; set; }

    // asc or desc. defaults to ascending
    public string? Order { get; set; }
}
