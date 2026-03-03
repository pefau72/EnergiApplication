namespace Application.SSO;

public interface ITokenProvider
{
    Task<string> GetAccessTokenAsync(CancellationToken cancellationToken);
}
