namespace EnergiApp.Domain
{

    public class Trade
    {
        public string TradeId { get; set; }
        public string ContractId { get; set; }
        public DateTime DeliveryStart { get; set; }
        public DateTime DeliveryEnd { get; set; }
        public decimal Volume { get; set; }
        public decimal Price { get; set; }
        public TradeSide Side { get; set; }
        public AuctionResultState Status { get; set; }
    }
}