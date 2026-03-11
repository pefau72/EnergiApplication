
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
    public void GenerateOneCurvePerOrder()
    {
        var auction = new Auction
        {
            id = "12129",
            contracts = new List<Contract>
        {
            new Contract { Id = "20230" },
            new Contract { Id = "20422" }
        }
        };

        var curves = OrderGenerator.GenerateCurves(auction, -500, -1500).ToList();

        Assert.Equal(2, curves.Count);
        Assert.Contains(curves, c => c.ContractId == "20230");
        Assert.Contains(curves, c => c.ContractId == "20422");
    }

 



}



