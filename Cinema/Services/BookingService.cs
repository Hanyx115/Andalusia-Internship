using Cinema.DTO;
using Cinema.Middleware;
using Cinema.Models;
using Cinema.Repistories.Interfaces;
using Cinema.Services.Interfaces;

namespace Cinema.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly ICustomerService _customerService;
    private readonly IShowTimeService _showTimeService;

    public BookingService(
        IBookingRepository bookingRepository,
        ICustomerService customerService,
        IShowTimeService showTimeService)
    {
        _bookingRepository = bookingRepository;
        _customerService = customerService;
        _showTimeService = showTimeService;
    }

    public async Task<PagedResult<Booking>> GetAllAsync(BookingFilterParams filter)
        => await _bookingRepository.GetAllAsync(filter);

    public async Task<Booking> GetByIdAsync(int id)
    {
        var booking = await _bookingRepository.GetByIdAsync(id);
        if (booking is null)
            throw new BookingNotFoundException($"Booking with ID {id} was not found.");

        return booking;
    }

    public async Task<Booking> CreateAsync(CreateBookingRequest request)
    {
        // Business Rule: A booking must belong to a valid guest customer
        var customer = await _customerService.GetByIdAsync(request.CustomerId);

        // Business Rule: A booking cannot be created for a showtime that is not available
        var showTime = await _showTimeService.GetByIdAsync(request.ShowTimeId);

        // Business Rule: A movie can only be scheduled according to its cineama availability
        if (!showTime.Movie.AvailableInCinema)
            throw new InvalidBookingException(
                $"Cannot book ShowTime {request.ShowTimeId} because '{showTime.Movie.Name}' is not currently available in the cinema.");

        var now = DateTime.UtcNow;
        var booking = new Booking
        {
            CustomerId = request.CustomerId,
            ShowTimeId = request.ShowTimeId,
            BookingDate = now,
            // Business Rule: Booking status is controlled by application
            Status = Booking.BookingStatus.Pending,
            CreatedAt = now,
            UpdatedAt = now,

            Customer = customer,
            ShowTime = showTime
        };

        return await _bookingRepository.AddAsync(booking);
    }

    public async Task<Booking> ConfirmAsync(int id)
    {
        var booking = await _bookingRepository.GetByIdAsync(id);
        if (booking is null)
            throw new BookingNotFoundException($"Booking with ID {id} was not found.");

        if (booking.Status != Booking.BookingStatus.Pending)
            throw new InvalidBookingException(
                $"Booking with ID {id} cannot be confirmed because it is currently {booking.Status}.");

        booking.Status = Booking.BookingStatus.Confirmed;
        booking.UpdatedAt = DateTime.UtcNow;

        await _bookingRepository.UpdateAsync(booking);
        return booking;
    }

    public async Task<Booking> CancelAsync(int id)
    {
        var booking = await _bookingRepository.GetByIdAsync(id);
        if (booking is null)
            throw new BookingNotFoundException($"Booking with ID {id} was not found.");

        if (booking.Status == Booking.BookingStatus.Cancelled)
            throw new InvalidBookingException($"Booking with ID {id} is already cancelled.");

        booking.Status = Booking.BookingStatus.Cancelled;
        booking.UpdatedAt = DateTime.UtcNow;

        await _bookingRepository.UpdateAsync(booking);
        return booking;
    }

    public async Task<IEnumerable<Booking>> GetByCustomerAsync(int customerId)
    {
        // Confirms the customer exists first
        await _customerService.GetByIdAsync(customerId);

        return await _bookingRepository.GetByCustomerIdAsync(customerId);
    }

    public async Task<IEnumerable<Booking>> GetByShowTimeAsync(int showTimeId)
    {
        //confirms showtime before booking
        await _showTimeService.GetByIdAsync(showTimeId);

        return await _bookingRepository.GetByShowTimeIdAsync(showTimeId);
    }
}
