namespace EnergiApp.Domain.Services
{
    public interface ICurveOrderService
    {
        CurveOrderRequest GenerateStaticCurveOrderRequest(
            string portfolio,
            string areaCode,
            Auction auction,
            double minPrice = -500.0d,
            double maxPrice = -1500.0d);
    }
}
