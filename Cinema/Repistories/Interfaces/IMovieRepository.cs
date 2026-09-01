using Cinema.DTO;
using Cinema.Models;

namespace Cinema.Repistories.Interfaces
{
    public interface IMovieRepository
    {
        Task<PagedResult<Movie>> GetAllAsync(MovieFilterParams filter);
        Task<Movie?> GetByIdAsync(int id);
        Task<bool> ExistsByNameAsync(string name);
        Task<Movie> AddAsync(Movie movie);
        Task UpdateAsync(Movie movie);
        Task DeleteAsync(Movie movie);
        Task<bool> HasShowTimesAsync(int movieId);

    }
}
