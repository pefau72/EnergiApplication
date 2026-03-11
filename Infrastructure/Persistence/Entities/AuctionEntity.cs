using System;
using System.Collections.Generic;
using System.Text;
using EnergiApp.Domain;

namespace EnergiApp.Infrastructure.Persistence.Entities
{
    public class AuctionEntity
    {   // Each of these elements should correspond to a field in the auctions table in the db.
        public Guid Id { get; set; }
        public string Name { get; set; }
        public AuctionStateType State { get; set; }
        public DateTime CloseForBidding { get; set; }
        public DateTime DeliveryStart { get; set; }
        public DateTime DeliveryEnd { get; set; }
        public List<ContractEntity> Contracts { get; set; } = new();
        public List<CurrencyEntity> Currencies { get; set; } = new();
        public List<PortfolioEntity> Portfolios { get; set; } = new();
        
        public DateTime RetrievedAtUtc { get; set; }

    }



}
