using Cinema.DTO;
using Cinema.Models;


namespace Cinema.Services.Interfaces;

public interface IMovieService
{
    Task<PagedResult<Movie>> GetAllAsync(MovieFilterParams filter);
    Task<Movie> GetByIdAsync(int id);
    Task<Movie> CreateAsync(CreateMovieRequest request);
    Task<Movie> UpdateAsync(int id, UpdateMovieRequest request);
    Task DeleteAsync(int id);
}
