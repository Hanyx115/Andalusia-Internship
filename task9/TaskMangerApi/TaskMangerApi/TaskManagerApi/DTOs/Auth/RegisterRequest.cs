namespace TaskManagerApi.DTOs.Auth;

public class RegisterRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    // Role and Id are deliberately not client-settable.
}
