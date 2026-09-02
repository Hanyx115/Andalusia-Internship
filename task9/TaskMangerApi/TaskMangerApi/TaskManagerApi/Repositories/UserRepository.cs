using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.Models;

namespace TaskManagerApi.Repositories;

public sealed class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task<AppUser?> GetByEmailAsync(string normalizedEmail, CancellationToken ct) =>
        await context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.NormalizedEmail == normalizedEmail, ct);

    public async Task<AppUser?> GetByIdAsync(int id, CancellationToken ct) =>
        await context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Id == id, ct);

    public async Task<bool> TryAddAsync(AppUser user, CancellationToken ct)
    {
        await context.Users.AddAsync(user, ct);
        try
        {
            await context.SaveChangesAsync(ct);
            return true;
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException { Number: 2601 or 2627 })
        {
            // The unique database index also handles simultaneous registrations.
            context.Entry(user).State = EntityState.Detached;
            return false;
        }
    }
}
