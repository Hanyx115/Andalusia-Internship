using Cinema.Models;
using Cinema.Repistories.Interfaces;
using Cinema.Data;
using Cinema.DTO;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Repistories
{
    public class ShowTimeRepository : IShowTimeRepository
    {
        private readonly ApplicationDbContext _context;

        public ShowTimeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // showtime by movie w hall
        public async Task<IEnumerable<ShowTime>> GetAllAsync()
            => await _context.ShowTimes
                .Include(s => s.Movie)
                .Include(s => s.Auditorium)
                .ToListAsync();

        public async Task<ShowTime?> GetByIdAsync(int id)
            => await _context.ShowTimes
                .Include(s => s.Movie)
                .Include(s => s.Auditorium)
                .FirstOrDefaultAsync(s => s.Id == id);

        public async Task<ShowTime> AddAsync(ShowTime showTime)
        {
            _context.ShowTimes.Add(showTime);
            await _context.SaveChangesAsync();
            return showTime;
        }

        public async Task UpdateAsync(ShowTime showTime)
        {
            _context.ShowTimes.Update(showTime);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ShowTime showTime)
        {
            _context.ShowTimes.Remove(showTime);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> HasBookingsAsync(int showTimeId)
            => await _context.Bookings.AnyAsync(b => b.ShowTimeId == showTimeId);

        public async Task<IEnumerable<ShowTime>> GetByAuditoriumAsync(int auditoriumId, DateTime? date)
        {
            var query = _context.ShowTimes
                .Include(s => s.Movie)
                .Include(s => s.Auditorium)
                .Where(s => s.AuditoriumId == auditoriumId);

            if (date.HasValue)
                query = query.Where(s => s.ShowDateTime.Date == date.Value.Date);

            return await query.ToListAsync();
        }
    }

}
