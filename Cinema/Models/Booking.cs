namespace Cinema.Models
{
    public class Booking
    {
        // Enum to represent the status of a booking
        public enum BookingStatus
        {
            Pending,
            Confirmed,
            Cancelled
        }
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public BookingStatus Status { get; set; }

        public int CustomerId { get; set; }
        public int ShowTimeId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public Customer Customer { get; set; } = null!;
        public ShowTime ShowTime { get; set; } = null!;
    }
}
