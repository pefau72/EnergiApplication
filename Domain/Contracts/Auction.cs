namespace EnergiApp.Domain
{
        public class Auction
    {
        public enum AuctionStateType { Open,Closed,Resultspublished,Cancelled };
        public string Id { get; set; }
        
        public string Name { get; set; }
        public AuctionStateType State { get; set; }
        public DateTime CloseForBidding { get; set; }
        public DateTime DeliveryStart { get; set; }
        public DateTime DeliveryEnd { get; set; }
        public List<Contract> Contracts { get; set; }
        public List<Currency> Currencies { get; set; }
        public List<Portfolio> Portfolios { get; set; }
    }
}