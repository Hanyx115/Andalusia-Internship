namespace TaskManagerApi.DTOs.Auth;

public record AuthUserDto(int Id, string Email, string Role);
public record LoginResponse(string Token, DateTime ExpiresAt, AuthUserDto User);
public record ApiMessage(string Message);
