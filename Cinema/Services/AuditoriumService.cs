using AutoMapper;
using Cinema.DTO;
using Cinema.Middleware;
using Cinema.Models;
using Cinema.Repistories.Interfaces;
using Cinema.Services.Interfaces;

namespace Cinema.Services;

public class AuditoriumService : IAuditoriumService
{
    private readonly IAuditoriumRepository _auditoriumRepository;
    private readonly IMapper _mapper;

    public AuditoriumService(IAuditoriumRepository auditoriumRepository, IMapper mapper)
    {
        _auditoriumRepository = auditoriumRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Auditorium>> GetAllAsync()
        => await _auditoriumRepository.GetAllAsync();

    public async Task<Auditorium> GetByIdAsync(int id)
    {
        var auditorium = await _auditoriumRepository.GetByIdAsync(id);
        if (auditorium is null)
            throw new AuditoriumNotFoundException($"Auditorium with ID {id} was not found.");

        return auditorium;
    }

    public async Task<Auditorium> CreateAsync(CreateAuditoriumRequest request)
    {
        var auditorium = _mapper.Map<Auditorium>(request);

        var now = DateTime.UtcNow;
        auditorium.CreatedAt = now;
        auditorium.UpdatedAt = now;

        return await _auditoriumRepository.AddAsync(auditorium);
    }

    public async Task<Auditorium> UpdateAsync(int id, UpdateAuditoriumRequest request)
    {
        var existing = await _auditoriumRepository.GetByIdAsync(id);
        if (existing is null)
            throw new AuditoriumNotFoundException($"Auditorium with ID {id} was not found.");

        existing.RoomNumber = request.RoomNumber;
        existing.Capacity = request.Capacity;
        existing.Available = request.Available;
        existing.UpdatedAt = DateTime.UtcNow;

        await _auditoriumRepository.UpdateAsync(existing);
        return existing;
    }

    public async Task DeleteAsync(int id)
    {
        var existing = await _auditoriumRepository.GetByIdAsync(id);
        if (existing is null)
            throw new AuditoriumNotFoundException($"Auditorium with ID {id} was not found.");

        if (await _auditoriumRepository.HasShowTimesAsync(id))
            throw new DeleteConflictException(
                $"Auditorium with ID {id} cannot be deleted because it has one or more showtimes scheduled.");

        await _auditoriumRepository.DeleteAsync(existing);
    }
}
