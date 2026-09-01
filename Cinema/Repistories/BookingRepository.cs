using Cinema.Data;
using Cinema.DTO;
using Cinema.Models;
using Cinema.Repistories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Repistories
{

    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        private IQueryable<Booking> IncludedBookings()
            => _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.ShowTime).ThenInclude(s => s.Movie)
                .Include(b => b.ShowTime).ThenInclude(s => s.Auditorium);

        public async Task<PagedResult<Booking>> GetAllAsync(BookingFilterParams filter)
        {
            var query = IncludedBookings();

            if (filter.CustomerId.HasValue)
                query = query.Where(b => b.CustomerId == filter.CustomerId.Value);

            if (!string.IsNullOrWhiteSpace(filter.CustomerName))
                query = query.Where(b => b.Customer.Name.Contains(filter.CustomerName));

            if (filter.ShowTimeId.HasValue)
                query = query.Where(b => b.ShowTimeId == filter.ShowTimeId.Value);

            if (filter.Status.HasValue)
                query = query.Where(b => b.Status == filter.Status.Value);

            var totalCount = await query.CountAsync();

            var data = await query
                .OrderByDescending(b => b.BookingDate)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return new PagedResult<Booking>
            {
                Data = data,
                Page = filter.Page,
                PageSize = filter.PageSize,
                TotalCount = totalCount
            };
        }

        public async Task<Booking?> GetByIdAsync(int id)
            => await IncludedBookings().FirstOrDefaultAsync(b => b.Id == id);

        public async Task<Booking> AddAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task UpdateAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Booking>> GetByCustomerIdAsync(int customerId)
            => await IncludedBookings().Where(b => b.CustomerId == customerId).ToListAsync();

        public async Task<IEnumerable<Booking>> GetByShowTimeIdAsync(int showTimeId)
            => await IncludedBookings().Where(b => b.ShowTimeId == showTimeId).ToListAsync();

        public async Task<bool> HasBookingsAsync(int customerId)
            => await _context.Bookings.AnyAsync(b => b.CustomerId == customerId);
       
    }
}