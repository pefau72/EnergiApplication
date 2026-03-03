namespace EnergiApp.Application.SSO 
{
    using Refit;

    public class TokenRequest
    {
        [AliasAs("grant_type")] public string GrantType { get; set; }

        [AliasAs("scope")] public string Scope { get; set; }

        [AliasAs("username")] public string Username { get; set; }

        [AliasAs("password")] public string Password { get; set; }

        [AliasAs("ClientID")] public string ClientID { get; set; }

        [AliasAs("Client_secret")] public string Client_secret { get; set; }
    }
}