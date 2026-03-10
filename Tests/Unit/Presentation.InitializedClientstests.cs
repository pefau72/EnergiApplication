using Docker.DotNet.Models;
using EnergiApp.Application;
using EnergiApp.Application.Utils;
using EnergiApp.Domain.Repositories;
using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using Xunit;
using static Runner; // Assuming InitializeClients is an internal method of Runner, we can use reflection to call it in the test.

public class GetCorrectInjectionTest
{
    

    [Fact] // For Xunit: This is a test method
    public void GetCorrectConfiguration()
    {
        // Arrange
        var ssoOptions = Options.Create(new NordPoolSsoOptions { BaseUrl = "https://sso.test/" }); //fake options, because not loaded from appsettings in test
        var apiOptions = Options.Create(new NordPoolApiOptions { BaseUrl = "https://api.test/" }); // Runner constructor requires IOptions, so we wrap our options in Options.Create

        var auctionRepository = new Mock<IAuctionRepository>(); // Mock the repository, as it's a dependency of Runner but not relevant for this test
        var refitFactory = new Mock<IRefitClientFactory>();
        var ssoClient = new Mock<INordPoolSsoClient>();
        var apiClient = new Mock<INordPoolApiClient>();

        HttpClient capturedSsoHttpClient = null; // 
        HttpClient capturedApiHttpClient = null;

        refitFactory //When Runner calls CreateClient<INordPoolSsoClient>(...), Capture the HttpClient passed in, Return your fake SSO client
            .Setup(f => f.CreateClient<INordPoolSsoClient>(It.IsAny<HttpClient>()))
            .Callback<HttpClient>(c => capturedSsoHttpClient = c)
            .Returns(ssoClient.Object);

        refitFactory
            .Setup(f => f.CreateClient<INordPoolApiClient>(It.IsAny<HttpClient>()))
            .Callback<HttpClient>(c => capturedApiHttpClient = c)
            .Returns(apiClient.Object);

        // System under test (SUT) - create an instance of Runner with the mocked dependencies
        var sut = new Runner(apiOptions, ssoOptions, auctionRepository.Object, refitFactory.Object);
                
        // Act
        sut.InitializeClients(); // expose InitializeClients via internal method or reflection

        // Assert
        Assert.Equal("https://sso.test/", capturedSsoHttpClient.BaseAddress.ToString());                  // Tests 1 + 2 
        Assert.Equal("https://api.test/", capturedApiHttpClient.BaseAddress.ToString());                  // Test 1 + 2

        var handler = capturedApiHttpClient.GetHandler();
        Assert.IsType<AuthenticatedHttpClientHandler>(handler);                                           // Test 3 see helper below

        refitFactory.Verify(f => f.CreateClient<INordPoolSsoClient>(It.IsAny<HttpClient>()), Times.Once); // Tests 4 + 5
        refitFactory.Verify(f => f.CreateClient<INordPoolApiClient>(It.IsAny<HttpClient>()), Times.Once); // Tests 4 + 5
    }
}

public static class HttpClientExtensions
{
    public static HttpMessageHandler? GetHandler(this HttpClient client)
    {
        var field = typeof(HttpMessageInvoker)
            .GetField("_handler", BindingFlags.NonPublic | BindingFlags.Instance); // .Getfield uses reflection to access the private field "_handler" of HttpMessageInvoker

        return field?.GetValue(client) as HttpMessageHandler;
    }
}

