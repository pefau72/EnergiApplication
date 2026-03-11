namespace EnergiApp.Domain
{
        public class Auction
    {
        public string id { get; set; }
          
        
        public string name { get; set; }
        public enum AuctionStateType { Open, Closed, Resultspublished, Cancelled };
        public AuctionStateType state { get; set; }
        public DateTime closeForBidding { get; set; }
        public DateTime deliveryStart { get; set; }
        public DateTime deliveryEnd { get; set; }
        public bool volumeAllocation { get; set; }
        public List<OrderType> availableOrderTypes { get; set; }
        public List<Currency> currencies { get; set; }
        public List<Contract> contracts { get; set; }
        
        public List<Portfolio> portfolios { get; set; }
    }
}