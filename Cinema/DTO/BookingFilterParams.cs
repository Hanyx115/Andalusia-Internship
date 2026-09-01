using Cinema.Models;
namespace Cinema.DTO;

public class BookingFilterParams : PaginationParams
{
    public int? CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public int? ShowTimeId { get; set; }
    public Booking.BookingStatus? Status { get; set; }
}
