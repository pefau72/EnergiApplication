namespace EnergiApp.Application.SSO;

public sealed class TokenResponse
{
    public string access_token { get; init; } = string.Empty;

    // Returned by Nord Pool (seconds)
    public int expires_in { get; init; }

    public string token_type { get; init; } = string.Empty;

    // Computed when the token is created

    //public bool IsExpired =>   DateTime.UtcNow >= ExpiresAtUtc - SafetyMargin;

}


