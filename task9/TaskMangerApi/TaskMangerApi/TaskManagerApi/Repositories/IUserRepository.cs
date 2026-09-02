using TaskManagerApi.Models;

namespace TaskManagerApi.Repositories;

public interface IUserRepository
{
    Task<AppUser?> GetByEmailAsync(string normalizedEmail, CancellationToken ct);
    Task<AppUser?> GetByIdAsync(int id, CancellationToken ct);
    Task<bool> TryAddAsync(AppUser user, CancellationToken ct);
}
