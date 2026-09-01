using AutoMapper;
using Cinema.DTO;
using Cinema.Middleware;
using Cinema.Models;
using Cinema.Repistories.Interfaces;
using Cinema.Services.Interfaces;

namespace Cinema.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IMapper _mapper;

    public MovieService(IMovieRepository movieRepository, IMapper mapper)
    {
        _movieRepository = movieRepository;
        _mapper = mapper;
    }


    //error can't fix
    public async Task<PagedResult<Movie>> GetAllAsync(MovieFilterParams filter)
        => await _movieRepository.GetAllAsync(filter);

    public async Task<Movie> GetByIdAsync(int id)
    {
        var movie = await _movieRepository.GetByIdAsync(id);
        if (movie is null)
            throw new MovieNotFoundException($"Movie with ID {id} was not found.");

        return movie;
    }

    public async Task<Movie> CreateAsync(CreateMovieRequest request)
    {
        if (await _movieRepository.ExistsByNameAsync(request.Name))
            throw new MovieAlreadyExistsException(
                $"A movie named '{request.Name}' already exists.");

        var movie = _mapper.Map<Movie>(request);

        var now = DateTime.UtcNow;
        movie.CreatedAt = now;
        movie.UpdatedAt = now;

        return await _movieRepository.AddAsync(movie);
    }

    public async Task<Movie> UpdateAsync(int id, UpdateMovieRequest request)
    {
        var existing = await _movieRepository.GetByIdAsync(id);
        if (existing is null)
            throw new MovieNotFoundException($"Movie with ID {id} was not found.");

        var nameIsChanging = !string.Equals(existing.Name, request.Name, StringComparison.Ordinal);
        if (nameIsChanging && await _movieRepository.ExistsByNameAsync(request.Name))
            throw new MovieAlreadyExistsException(
                $"A movie named '{request.Name}' already exists.");

        existing.Name = request.Name;
        existing.Genre = request.Genre;
        existing.ReleaseDate = request.ReleaseDate;
        existing.AvailableInCinema = request.AvailableInCinema;
        existing.UpdatedAt = DateTime.UtcNow;

        await _movieRepository.UpdateAsync(existing);
        return existing;
    }

    public async Task DeleteAsync(int id)
    {
        var existing = await _movieRepository.GetByIdAsync(id);
        if (existing is null)
            throw new MovieNotFoundException($"Movie with ID {id} was not found.");

        if (await _movieRepository.HasShowTimesAsync(id))
            throw new DeleteConflictException(
                $"Movie with ID {id} cannot be deleted because it has one or more showtimes scheduled.");

        await _movieRepository.DeleteAsync(existing);
    }
}
