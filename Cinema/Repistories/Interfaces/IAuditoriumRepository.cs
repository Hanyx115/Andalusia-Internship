using Cinema.Models;

namespace Cinema.Repistories.Interfaces
{
    public interface IAuditoriumRepository
    {
        Task<IEnumerable<Auditorium>> GetAllAsync();
        Task<Auditorium?> GetByIdAsync(int id);
        Task<Auditorium> AddAsync(Auditorium auditorium);
        Task UpdateAsync(Auditorium auditorium);
        Task DeleteAsync(Auditorium auditorium);
        Task<bool> HasShowTimesAsync(int auditoriumId);
    }
}
