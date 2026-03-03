namespace EnergiApp.Application
{
    using EnergiApp.Domain;
    using EnergiApp.Application.SSO;
    using Refit;
    using System.Threading.Tasks;
    
    public interface INordPoolSsoClient
    {
        [Post("/connect/token")]
        Task<TokenResponse> PostTokenAsync([Header("Authorization")] string basicAuth,
            [Body(BodySerializationMethod.UrlEncoded)]
            TokenRequest tokenRequest);


    }
    
}