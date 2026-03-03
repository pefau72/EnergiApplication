namespace EnergiApp.Domain
{

    public class TradesSummary
    {
        public string AreaCode { get; set; }
        public string AuctionId { get; set; }
        public string OrderId { get; set; }
        public string CompanyName { get; set; }
        public string Portfolio { get; set; }
        public string CurrencyCode { get; set; }
        public string UserId { get; set; }
        public OrderResultType OrderResultType { get; set; }
        public string Name { get; set; }
        public string ExclusiveGroup { get; set; }
        public List<Trade> Trades { get; set; }
    }
       
}