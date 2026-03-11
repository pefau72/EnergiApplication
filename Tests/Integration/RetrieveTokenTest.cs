using Application.SSO;
using EnergiApp.Application;
using EnergiApp.Application.Utils;
using Microsoft.Extensions.Options;
using Refit;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;


namespace Tests.Integration
{
    public class RetrieveTokenTest
    {
       
        [Fact] // This test method will be executed by the test runner
        public async Task RetrieveTokenTest_imp()
        {
            var server = WireMockServer.Start(); // Start a WireMock server on a random port

            server // Configure the server to respond to POST requests to /connect/token with a specific JSON response
                .Given(Request.Create().WithPath("/connect/token").UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBody("{ \"access_token\": \"abc123\", \"expires_in\": 3600, \"token_type\": \"00:00:00\" }"));

            var http = new HttpClient { BaseAddress = new Uri(server.Url) }; // Create an HttpClient with the base address of the WireMock server
            var ssoClient = RestService.For<INordPoolSsoClient>(http); // Create a Refit client for the INordPoolSsoClient interface using the HttpClient

            var options = Options.Create(new NordPoolSsoOptions // Create an instance of NordPoolSsoOptions with test values
            {
                username = "Test",
                password = "None",
                BaseUrl = "https://sts.test.nordpoolgroup.com"
                
            });

            var provider = new NordPoolTokenProvider(ssoClient, options); // Create an instance of NordPoolTokenProvider using the Refit client and options

            var token = await provider.GetAccessTokenAsync(TestContext.Current.CancellationToken); // Call the GetAccessTokenAsync method to retrieve the access token

            Assert.Equal("abc123", token); // Assert that the retrieved token matches the expected value
            server.Stop(); // Stop the WireMock server after the test is complete  

        }


    }
}
