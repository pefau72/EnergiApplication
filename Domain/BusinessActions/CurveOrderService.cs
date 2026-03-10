namespace EnergiApp.Domain.Services
{
    public class CurveOrderService : ICurveOrderService
    {
        public CurveOrderRequest GenerateStaticCurveOrderRequest(
            string portfolio,
            string areaCode,
            Auction auction,
            double minPrice = -500.0d,
            double maxPrice = -1500.0d)
        {
            return new CurveOrderRequest
            {
                AreaCode = areaCode,
                Portfolio = portfolio,
                AuctionId = auction.Id,
                Comment = $"CurveOrder_{areaCode}_{portfolio}",
                Curves = GenerateCurves(auction, minPrice, maxPrice).ToList()
            };
        }

        private IEnumerable<Curve> GenerateCurves(
            Auction auction, double minPrice, double maxPrice)
        {
            // your existing logic here
            throw new NotImplementedException();
        }
    }
}
