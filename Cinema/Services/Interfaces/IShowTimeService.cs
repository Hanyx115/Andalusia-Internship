using Cinema.DTO;
using Cinema.Models;

namespace Cinema.Services.Interfaces;

public interface IShowTimeService
{
    Task<IEnumerable<ShowTime>> GetAllAsync();
    Task<ShowTime> GetByIdAsync(int id);
    Task<ShowTime> CreateAsync(CreateShowTimeRequest request);
    Task<ShowTime> UpdateAsync(int id, UpdateShowTimeRequest request);
    Task DeleteAsync(int id);
    Task<IEnumerable<ShowTime>> GetByAuditoriumAsync(int auditoriumId, DateTime? date);
}
