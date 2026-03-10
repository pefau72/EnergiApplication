namespace EnergiApp.Presentation.Utils
{
    using System.Collections.Generic;
    using System.Linq;
    using EnergiApp.Domain;

    public static class OrderGenerator
    {
        public static CurveOrderRequest GenerateStaticCurveOrderRequest(string portfolio, string areaCode,
            Auction auction, double minPrice = -500.0d, double maxPrice = -1500.0d)
        {
            var curveOrderRequest = new CurveOrderRequest
            {
                AreaCode = areaCode,
                Portfolio = portfolio,
                AuctionId = auction.Id,
                Comment = $"CurveOrder_{areaCode}_{portfolio}",
                Curves = GenerateCurves(auction, minPrice, maxPrice).ToList()
            };

            return curveOrderRequest;
        }

    
        public static IEnumerable<Curve> GenerateCurves(Auction auction, double minPrice, double maxPrice)
        {
            var curves = new List<Curve>();
            foreach (var contract in auction.Contracts)
            {
                var curvePoints = GenerateCurvePoints(minPrice, maxPrice);
                curves.Add(new Curve
                {
                    ContractId = contract.Id,
                    CurvePoints = curvePoints.ToList()
                });
            }

            return curves;
        }

        private static IEnumerable<CurvePoint> GenerateCurvePoints(double minPrice, double maxPrice)
        {
            var curvePoints = new List<CurvePoint>();
            var startingVolume = 500;
            var numberOfPriceSteps = 20;
            var totalPriceDifference = maxPrice - minPrice;
            var priceStep = totalPriceDifference / numberOfPriceSteps;
            var volumeStep = 50;

            // Add min price point
            curvePoints.Add(new CurvePoint
            {
                Price = minPrice,
                Volume = startingVolume
            });

            for (var i = 1; i <= numberOfPriceSteps; i++)
            {
                var curvePoint = new CurvePoint
                {
                    Price = minPrice + i * priceStep,
                    Volume = startingVolume - (i - 1) * volumeStep
                };
                curvePoints.Add(curvePoint);
            }

            return curvePoints;
        }
    }
      
}