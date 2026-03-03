namespace EnergiApp.Application.SSO;

public sealed class TokenResponse
{
    public string AccessToken { get; init; } = string.Empty;

    // Returned by Nord Pool (seconds)
    public int ExpiresIn { get; init; }

    // Computed when the token is created
    public DateTime ExpiresAtUtc { get; init; }
    public DateTime ObtainedAt { get; init; }

    private static readonly TimeSpan SafetyMargin = TimeSpan.FromSeconds(30);

    //public bool IsExpired =>   DateTime.UtcNow >= ExpiresAtUtc - SafetyMargin;
    public bool IsExpired => DateTimeOffset.UtcNow >= ObtainedAt.AddSeconds(ExpiresIn - 30);
}
