using Cinema.DTO;
using Cinema.Models;
namespace Cinema.Services.Interfaces;

public interface IAuditoriumService
{
    Task<IEnumerable<Auditorium>> GetAllAsync();
    Task<Auditorium> GetByIdAsync(int id);
    Task<Auditorium> CreateAsync(CreateAuditoriumRequest request);
    Task<Auditorium> UpdateAsync(int id, UpdateAuditoriumRequest request);
    Task DeleteAsync(int id);
}
