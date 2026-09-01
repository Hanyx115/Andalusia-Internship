namespace Cinema.Models
{
    public class ShowTime
    {
        public int Id { get; set; }
        public DateTime ShowDateTime { get; set; }
        public int MovieId { get; set; }
        public int AuditoriumId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        // Navigation properties
        public Movie Movie { get; set; } = null!;
        public Auditorium Auditorium { get; set; } = null!;
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}

