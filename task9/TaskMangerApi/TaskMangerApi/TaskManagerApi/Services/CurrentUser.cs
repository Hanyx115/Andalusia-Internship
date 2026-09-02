using System.Globalization;

namespace TaskManagerApi.Services;

public sealed class CurrentUser(IHttpContextAccessor accessor) : ICurrentUser
{
    public int UserId
    {
        get
        {
            var principal = accessor.HttpContext?.User;
            if (principal?.Identity?.IsAuthenticated != true ||
                !int.TryParse(principal.FindFirst("sub")?.Value,
                    NumberStyles.None, CultureInfo.InvariantCulture, out var id) || id <= 0)
                throw new UnauthorizedAccessException("A valid authenticated user is required.");
            return id;
        }
    }
}
