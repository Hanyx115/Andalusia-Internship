using AutoMapper;
using Cinema.DTO;
using Cinema.Middleware;
using Cinema.Models;
using Cinema.Repistories.Interfaces;
using Cinema.Services.Interfaces;

namespace Cinema.Api.Services;

public class ShowTimeService : IShowTimeService
{
    private readonly IShowTimeRepository _showTimeRepository;
    private readonly IMovieService _movieService;
    private readonly IAuditoriumService _auditoriumService;
    private readonly IMapper _mapper;

    public ShowTimeService(
        IShowTimeRepository showTimeRepository,
        IMovieService movieService,
        IAuditoriumService auditoriumService,
        IMapper mapper)
    {
        _showTimeRepository = showTimeRepository;
        _movieService = movieService;
        _auditoriumService = auditoriumService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ShowTime>> GetAllAsync()
        => await _showTimeRepository.GetAllAsync();

    public async Task<ShowTime> GetByIdAsync(int id)
    {
        var showTime = await _showTimeRepository.GetByIdAsync(id);
        if (showTime is null)
            throw new ShowTimeNotFoundException($"ShowTime with ID {id} was not found.");

        return showTime;
    }

    public async Task<ShowTime> CreateAsync(CreateShowTimeRequest request)
    {
        // Business Rules: A movie referenced by a showtime must exist and An auditorium referenced by a showtime must exist
       
        await _movieService.GetByIdAsync(request.MovieId);
        await _auditoriumService.GetByIdAsync(request.AuditoriumId);

        var showTime = _mapper.Map<ShowTime>(request);

        var now = DateTime.UtcNow;
        showTime.CreatedAt = now;
        showTime.UpdatedAt = now;

        return await _showTimeRepository.AddAsync(showTime);
    }

    public async Task<ShowTime> UpdateAsync(int id, UpdateShowTimeRequest request)
    {
        var existing = await _showTimeRepository.GetByIdAsync(id);
        if (existing is null)
            throw new ShowTimeNotFoundException($"ShowTime with ID {id} was not found.");

        await _movieService.GetByIdAsync(request.MovieId);
        await _auditoriumService.GetByIdAsync(request.AuditoriumId);

        existing.ShowDateTime = request.ShowDateTime;
        existing.MovieId = request.MovieId;
        existing.AuditoriumId = request.AuditoriumId;
        existing.UpdatedAt = DateTime.UtcNow;

        await _showTimeRepository.UpdateAsync(existing);
        return existing;
    }

    public async Task DeleteAsync(int id)
    {
        var existing = await _showTimeRepository.GetByIdAsync(id);
        if (existing is null)
            throw new ShowTimeNotFoundException($"ShowTime with ID {id} was not found.");

        if (await _showTimeRepository.HasBookingsAsync(id))
            throw new DeleteConflictException(
                $"ShowTime with ID {id} cannot be deleted because it has one or more bookings.");

        await _showTimeRepository.DeleteAsync(existing);
    }

    public async Task<IEnumerable<ShowTime>> GetByAuditoriumAsync(int auditoriumId, DateTime? date)
    {
        // Confirms the auditorium itself exists before querying its showtimes
        // — throws AuditoriumNotFoundException if not, same reuse pattern.
        await _auditoriumService.GetByIdAsync(auditoriumId);

        return await _showTimeRepository.GetByAuditoriumAsync(auditoriumId, date);
    }
}
