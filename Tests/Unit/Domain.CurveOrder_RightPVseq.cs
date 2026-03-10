
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
    public void GenerateStaticCurveOrder_rightPVseq()
    {
        // Arrange
        double minPrice = -500;
        double maxPrice = -1500;

        // Act
        var points = OrderGenerator.GenerateCurvePoints(minPrice, maxPrice).ToList();

        // Assert: correct number of points
        Assert.Equal(21, points.Count);

        // Calculate expected step sizes
        double expectedPriceStep = (maxPrice - minPrice) / 20; // -50
        int expectedVolumeStep = 50;

        // Assert: first point
        Assert.Equal(minPrice, points[0].Price);
        Assert.Equal(500, points[0].Volume);

        // Assert: each subsequent point follows the pattern
        for (int i = 1; i < points.Count; i++)
        {
            double expectedPrice = minPrice + i * expectedPriceStep;
            int expectedVolume = 500 - (i - 1) * expectedVolumeStep;

            Assert.Equal(expectedPrice, points[i].Price, precision: 5);
            Assert.Equal(expectedVolume, points[i].Volume);
        }
    }
}



