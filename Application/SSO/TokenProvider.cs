using EnergiApp.Application;
using EnergiApp.Application.Utils;
using EnergiApp.Application.SSO;
using Microsoft.Extensions.Options;
using System.Text;

namespace Application.SSO;

public sealed class NordPoolTokenProvider : ITokenProvider
{
    private readonly INordPoolSsoClient _sso;
    private readonly NordPoolSsoOptions _options;
    private TokenResponse? _cached;
    private readonly SemaphoreSlim _lock = new(1, 1);

    public NordPoolTokenProvider(
        INordPoolSsoClient sso,
        IOptions<NordPoolSsoOptions> options)
    {
        _sso = sso;
        _options = options.Value;
    }

    
    public async Task<string> GetAccessTokenAsync(CancellationToken ct)
    {
        await _lock.WaitAsync(ct);
        try
        {
            if (_cached is null || _cached.IsExpired)
            {
                var basicAuth = Convert.ToBase64String(
                    Encoding.UTF8.GetBytes($"{_options.ClientId}:{_options.ClientSecret}")
                );

                var tokenRequest = new TokenRequest
                {
                    GrantType = "client_credentials",
                    Scope = _options.Scope
                };

                _cached = await _sso.PostTokenAsync($"Basic {basicAuth}", tokenRequest);
            }

            return _cached.AccessToken;
        }
        finally
        {
            _lock.Release();
        }
    }
}
