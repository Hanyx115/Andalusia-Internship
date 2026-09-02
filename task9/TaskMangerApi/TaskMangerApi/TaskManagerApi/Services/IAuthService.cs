using TaskManagerApi.DTOs.Auth;

namespace TaskManagerApi.Services;

public interface IAuthService
{
    Task<AuthUserDto?> RegisterAsync(RegisterRequest request, CancellationToken ct);
    Task<LoginResponse?> LoginAsync(LoginRequest request, CancellationToken ct);
}
