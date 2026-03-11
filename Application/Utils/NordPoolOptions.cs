namespace EnergiApp.Application.Utils
{
    public class NordPoolApiOptions
    {
        public string BaseUrl { get; set; } = "";
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string ClientId { get; set; } = "";
        public string ClientSecret { get; set; } = "";
    }


public sealed class NordPoolSsoOptions { 
        public string username { get; init; } = default!;
        public string password { get; init; } = default!; 
        public string Scope { get; init; } = "api.read"; 
        public string BaseUrl { get; init; } = default!;
    }
}

