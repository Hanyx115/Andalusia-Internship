using Cinema.Models;
using Cinema.Repistories.Interfaces;
using Cinema.Data;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Repistories;

public class AuditoriumRepository : IAuditoriumRepository
{
    private readonly ApplicationDbContext _context;

    public AuditoriumRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Auditorium>> GetAllAsync()
        => await _context.Auditoriums.ToListAsync();

    public async Task<Auditorium?> GetByIdAsync(int id)
        => await _context.Auditoriums.FindAsync(id);

    public async Task<Auditorium> AddAsync(Auditorium auditorium)
    {
        _context.Auditoriums.Add(auditorium);
        await _context.SaveChangesAsync();
        return auditorium;
    }

    public async Task UpdateAsync(Auditorium auditorium)
    {
        _context.Auditoriums.Update(auditorium);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Auditorium auditorium)
    {
        _context.Auditoriums.Remove(auditorium);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> HasShowTimesAsync(int auditoriumId)
        => await _context.ShowTimes.AnyAsync(s => s.AuditoriumId == auditoriumId);
}
