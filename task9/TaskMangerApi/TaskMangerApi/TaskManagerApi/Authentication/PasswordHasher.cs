namespace TaskManagerApi.Authentication;

public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string password, string hash);
    void VerifyDummy(string password);
}

public sealed class PasswordHasher : IPasswordHasher
{
    private const int WorkFactor = 12;
    // Unknown emails still do BCrypt work; never use this as an account password.
    private readonly string dummyHash = BCrypt.Net.BCrypt.HashPassword(
        Guid.NewGuid().ToString("N"), workFactor: WorkFactor);

    public string Hash(string password) =>
        BCrypt.Net.BCrypt.HashPassword(password, workFactor: WorkFactor);

    public bool Verify(string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);

    public void VerifyDummy(string password) => _ = Verify(password, dummyHash);
}
