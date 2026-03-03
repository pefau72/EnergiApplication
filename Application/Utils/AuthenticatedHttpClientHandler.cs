using Application.SSO;
using System.Net.Http.Headers;
namespace EnergiApp.Application.Utils
{
    public sealed class AuthenticatedHttpClientHandler : DelegatingHandler
    {
        private readonly ITokenProvider _tokenProvider;

        public AuthenticatedHttpClientHandler(ITokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var token = await _tokenProvider.GetAccessTokenAsync(cancellationToken);

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken);
        }

    }
}