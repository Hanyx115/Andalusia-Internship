using Cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
          DbContextOptions<ApplicationDbContext> options)
          : base(options)
        {
        }
        public DbSet<Movie> Movies => Set<Movie>();
        public DbSet<Auditorium> Auditoriums => Set<Auditorium>();
        public DbSet<ShowTime> ShowTimes => Set<ShowTime>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Booking> Bookings => Set<Booking>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // movie name and genre are required, and movie name must be unique
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(m => m.Name).IsRequired().HasMaxLength(200);
                entity.Property(m => m.Genre).IsRequired().HasMaxLength(100);

                // name must be unique
                entity.HasIndex(m => m.Name).IsUnique();
            });

            // auditorium room number is required
            modelBuilder.Entity<Auditorium>(entity =>
            {
                entity.Property(a => a.RoomNumber).IsRequired();
            });

            // one movie can have many showtimes, one auditorium can have many showtimes, but a showtime can only have one movie and one auditorium
            modelBuilder.Entity<ShowTime>(entity =>
            {
                entity.HasOne(s => s.Movie)
                      .WithMany(m => m.Shows)
                      .HasForeignKey(s => s.MovieId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(s => s.Auditorium)
                      .WithMany(a => a.Shows)
                      .HasForeignKey(s => s.AuditoriumId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(s => s.MovieId);
                entity.HasIndex(s => s.AuditoriumId);
            });

            // customer name and email are required
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(c => c.Name).IsRequired().HasMaxLength(200);
                entity.Property(c => c.Email).IsRequired().HasMaxLength(256);
            });

            // one customer can have many bookings, one showtime can have many bookings, but a booking can only have one customer and one showtime
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasOne(b => b.Customer)
                      .WithMany(c => c.Bookings)
                      .HasForeignKey(b => b.CustomerId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(b => b.ShowTime)
                      .WithMany(s => s.Bookings)
                      .HasForeignKey(b => b.ShowTimeId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(b => b.CustomerId);
                entity.HasIndex(b => b.ShowTimeId);
                entity.HasIndex(b => b.Status);
            });
        }



    }
}
