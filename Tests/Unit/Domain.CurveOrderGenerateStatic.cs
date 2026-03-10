
using EnergiApp.Application;
using EnergiApp.Domain;
using EnergiApp.Domain.BusinessActions;
using Moq;


using System.Collections.Generic;
using System.Linq;
using Xunit;


public partial class CurveOrderTests
{

    [Fact] // For Xunit: This is a test method
    public void GenerateStaticCurveOrder()
    {
        var auction = new Auction // Mock ikke nødvendig, da vi ikke kalder nogen metoder på
        {
            Id = "42",
            Contracts = new List<Contract>
        {
            new Contract { Id = "100" },
            new Contract { Id = "200" }
        }
        };

        string portfolio = "P1";
        string areaCode = "DK1";
        double minPrice = -500;
        double maxPrice = -1500;

        // Act
        var result = OrderGenerator.GenerateStaticCurveOrderRequest(
            portfolio,
            areaCode,
            auction,
            minPrice,
            maxPrice
        );

        // Assert: top-level fields
        Assert.Equal(areaCode, result.AreaCode);
        Assert.Equal(portfolio, result.Portfolio);
        Assert.Equal(auction.Id, result.AuctionId);
        Assert.Equal("CurveOrder_DK1_P1", result.Comment);

        // Assert: curves created correctly
        Assert.Equal(auction.Contracts.Count, result.Curves.Count);

        // Assert: each curve matches its contract
        foreach (var contract in auction.Contracts)
        {
            var curve = result.Curves.Single(c => c.ContractId == contract.Id);

            // Each curve should have 21 points (1 + 20 steps)
            Assert.Equal(21, curve.CurvePoints.Count);

            // First point should match minPrice and starting volume
            Assert.Equal(minPrice, curve.CurvePoints.First().Price);
            Assert.Equal(500, curve.CurvePoints.First().Volume);
        }
    }



  
}



