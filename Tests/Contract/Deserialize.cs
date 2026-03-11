using EnergiApp.Domain;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;





    public class NordPoolContractTests
    {
        [Fact]
        public void PortfolioArea_Deserializes_Correctly()
        {
        // Arrange
            var path = Path.Combine(AppContext.BaseDirectory, "Tests", "auctions.json");
            var json = File.ReadAllText(path);



        // Act
        var result = JsonSerializer.Deserialize<Auction>(json,
            new JsonSerializerOptions
            {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() 
            }
}
);

        // Assert
        Assert.NotNull(result);
            Assert.Equal("string", result.name);
            Assert.Equal("2026-03-11T18:48:20.572Z", result.deliveryEnd.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'"));
            Assert.Equal("string", result.currencies[0].CurrencyCode);
            Assert.Equal("string", result.portfolios[0].Areas[0].Name);
            

        }
    }



