using System.Text;
using TaskManagerApi.Authentication;
using TaskManagerApi.DTOs.Auth;
using TaskManagerApi.Models;
using TaskManagerApi.Repositories;

namespace TaskManagerApi.Services;

public sealed class AuthService(IUserRepository users, IPasswordHasher passwords,
    IJwtTokenService tokens, TimeProvider clock) : IAuthService
{
    public async Task<AuthUserDto?> RegisterAsync(RegisterRequest request, CancellationToken ct)
    {
        var email = request.Email.Trim();
        var normalizedEmail = email.ToUpperInvariant();
        if (await users.GetByEmailAsync(normalizedEmail, ct) is not null) return null;
        var user = new AppUser
        {
            Email = email,
            NormalizedEmail = normalizedEmail,
            PasswordHash = passwords.Hash(request.Password),
            Role = "User", // Never read a role from the request.
            CreatedAt = clock.GetUtcNow().UtcDateTime
        };
        if (!await users.TryAddAsync(user, ct)) return null;
        return new AuthUserDto(user.Id, user.Email, user.Role);
    }

    public async Task<LoginResponse?> LoginAsync(LoginRequest request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || request.Email.Length > 254 ||
            string.IsNullOrEmpty(request.Password) ||
            Encoding.UTF8.GetByteCount(request.Password) > 72)
            return null;

        var user = await users.GetByEmailAsync(request.Email.Trim().ToUpperInvariant(), ct);
        if (user is null)
        {
            passwords.VerifyDummy(request.Password);
            return null;
        }
        if (!passwords.Verify(request.Password, user.PasswordHash)) return null;
        return tokens.GenerateToken(user);
    }
}
