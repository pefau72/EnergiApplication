namespace EnergiApp.Domain
{

    public class CurveOrderRequest
    {
        public string AuctionId { get; set; }
        public string Portfolio { get; set; }
        public string AreaCode { get; set; }
        public string Comment { get; set; }
        public List<Curve> Curves { get; set; }
    }
}