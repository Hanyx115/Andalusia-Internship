using Cinema.Models;

namespace Cinema.Repistories.Interfaces
{
    public interface IShowTimeRepository
    {
        Task<IEnumerable<ShowTime>> GetAllAsync();
        Task<ShowTime?> GetByIdAsync(int id);
        Task<ShowTime> AddAsync(ShowTime showTime);
        Task UpdateAsync(ShowTime showTime);
        Task DeleteAsync(ShowTime showTime);
        Task<bool> HasBookingsAsync(int showTimeId);
        Task<IEnumerable<ShowTime>> GetByAuditoriumAsync(int auditoriumId, DateTime? date);
    }
}
