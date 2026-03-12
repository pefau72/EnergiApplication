
using EnergiApp.Application;
using EnergiApp.Infrastructure;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Xunit;

namespace Tests.Application;
public class GetCorrectInjectionTest
{
    

    [Fact] // For Xunit: This is a test method
    public void GetCorrectConfiguration()
    {
        // Arrange

        var config = new ConfigurationBuilder()
        .AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["NordPoolApi:BaseUrl"] = "https://api.test/",
            ["NordPoolSso:BaseUrl"] = "https://sso.test/"
        })
        .Build();

        var services = new ServiceCollection();

        services.AddApplication(config); // DI extension
        services.AddInfrastructure(config);

        var provider = services.BuildServiceProvider();

        // Act
        var apiClient = provider.GetRequiredService<INordPoolApiClient>();
        var ssoClient = provider.GetRequiredService<INordPoolSsoClient>();

        var apiHttpClientField = apiClient.GetType()
            .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
            .FirstOrDefault(f => f.FieldType == typeof(HttpClient));

        Assert.NotNull(apiHttpClientField);

        var apiHttpClient = (HttpClient)apiHttpClientField!.GetValue(apiClient)!;


        var ssoHttpClientField = ssoClient.GetType()
            .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
            .FirstOrDefault(f => f.FieldType == typeof(HttpClient));

        Assert.NotNull(ssoHttpClientField);

        var ssoHttpClient = (HttpClient)ssoHttpClientField!.GetValue(ssoClient)!;

        // Assert
        Assert.Equal("https://api.test/", apiHttpClient.BaseAddress!.ToString());
        Assert.Equal("https://sso.test/", ssoHttpClient.BaseAddress!.ToString());                 

        
    }
}



