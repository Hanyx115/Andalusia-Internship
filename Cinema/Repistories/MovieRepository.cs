using Cinema.Models;
using Cinema.Repistories.Interfaces;
using Cinema.Data;
using Cinema.DTO;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Repisitories;


public class MovieRepository : IMovieRepository
{
    private readonly ApplicationDbContext _context;

    public MovieRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<Movie>> GetAllAsync(MovieFilterParams filter)
    {
        var query = _context.Movies.AsQueryable();

        // Search by partial name 
        if (!string.IsNullOrWhiteSpace(filter.Search))
            query = query.Where(m => m.Name.Contains(filter.Search));

        // Filter by genre
        if (!string.IsNullOrWhiteSpace(filter.Genre))
            query = query.Where(m => m.Genre == filter.Genre);

        var totalCount = await query.CountAsync();


        query = (filter.SortBy?.ToLowerInvariant()) switch
        {
            "releasedate" => filter.Order?.ToLowerInvariant() == "desc"
                ? query.OrderByDescending(m => m.ReleaseDate)
                : query.OrderBy(m => m.ReleaseDate),
            _ => filter.Order?.ToLowerInvariant() == "desc"
                ? query.OrderByDescending(m => m.Name)
                : query.OrderBy(m => m.Name),
        };

        var data = await query
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        return new PagedResult<Movie>
        {
            Data = data,
            Page = filter.Page,
            PageSize = filter.PageSize,
            TotalCount = totalCount
        };
    }

    public async Task<Movie?> GetByIdAsync(int id)
        => await _context.Movies.FindAsync(id);

    public async Task<bool> ExistsByNameAsync(string name)
        => await _context.Movies.AnyAsync(m => m.Name == name);

    public async Task<Movie> AddAsync(Movie movie)
    {
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();
        return movie;
    }

    public async Task UpdateAsync(Movie movie)
    {
        _context.Movies.Update(movie);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Movie movie)
    {
        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> HasShowTimesAsync(int movieId)
        => await _context.ShowTimes.AnyAsync(s => s.MovieId == movieId);
}
