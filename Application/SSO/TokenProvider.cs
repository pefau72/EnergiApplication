using EnergiApp.Application;
using EnergiApp.Application.Utils;
using EnergiApp.Application.SSO;
using Microsoft.Extensions.Options;
using System.Text;
// thread‑safe, cached OAuth2 client_credentials token provider.
namespace Application.SSO;

public sealed class NordPoolTokenProvider : ITokenProvider
{
    private readonly INordPoolSsoClient _sso; // Refit REST API client for SSO interactions
    private readonly NordPoolSsoOptions _options; // Configuration options for SSO (client ID, secret, scope, etc.). via DI
    private TokenResponse? _cached;  // Cached token response. Will be null if no token has been fetched yet. Will be updated when a new token is fetched.
    private readonly SemaphoreSlim _lock = new(1, 1); // Semaphore to ensure thread-safe access to the token cache
                                                      // Only one thread will request a new token. The others will wait and reuse the cached token
    public NordPoolTokenProvider(                 // Constructor with dependencies injected via DI
        INordPoolSsoClient sso,
        IOptions<NordPoolSsoOptions> options)
    {
        _sso = sso;
        _options = options.Value;
    }

    
    public async Task<string> GetAccessTokenAsync(CancellationToken ct)
    {
        await _lock.WaitAsync(ct); // This ensures only one thread refreshes the token. 
        try
        {
            if (_cached is null || _cached.expires_in < 5) // If no token or expired → fetch new one.
            {
                var basicAuth = Convert.ToBase64String(  // Build Basic Auth header
                    Encoding.UTF8.GetBytes($"{_options.username}:{_options.password}")
                );

                var tokenRequest = new TokenRequest // Build request body for token endpoint
                {
                    GrantType = "client_credentials",
                    Scope = _options.Scope
                };

                _cached = await _sso.PostTokenAsync($"Basic {basicAuth}", tokenRequest); // Call SSO client to get new token. The response will be cached for future calls until it expires.
            }

            return _cached.access_token;
        }
        finally
        {
            _lock.Release();
        }
    }
}
