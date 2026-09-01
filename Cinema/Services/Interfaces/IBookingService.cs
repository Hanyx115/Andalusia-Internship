using Cinema.DTO;
using Cinema.Models;


namespace Cinema.Services.Interfaces;

public interface IBookingService
{
    Task<PagedResult<Booking>> GetAllAsync(BookingFilterParams filter);
    Task<Booking> GetByIdAsync(int id);
    Task<Booking> CreateAsync(CreateBookingRequest request);
    Task<Booking> ConfirmAsync(int id);
    Task<Booking> CancelAsync(int id);
    Task<IEnumerable<Booking>> GetByCustomerAsync(int customerId);
    Task<IEnumerable<Booking>> GetByShowTimeAsync(int showTimeId);
}
