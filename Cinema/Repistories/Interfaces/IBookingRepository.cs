using Cinema.DTO;
using Cinema.Models;

namespace Cinema.Repistories.Interfaces
{
    public interface IBookingRepository
    {
        Task<PagedResult<Booking>> GetAllAsync(BookingFilterParams filter);
        Task<Booking?> GetByIdAsync(int id);
        Task<Booking> AddAsync(Booking booking);
        Task UpdateAsync(Booking booking);
        Task<IEnumerable<Booking>> GetByCustomerIdAsync(int customerId);
        Task<IEnumerable<Booking>> GetByShowTimeIdAsync(int showTimeId);
        
    }
}
