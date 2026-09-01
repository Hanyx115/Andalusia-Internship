namespace Cinema.Models
{
    public class Auditorium
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public int Capacity { get; set; }
        public bool Available { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation property for the related ShowTime entitiess

        public ICollection<ShowTime> Shows { get; set; } = new List<ShowTime>();
    }
}
