using EnergiApp.Domain;
using EnergiApp.Infrastructure.Persistence.Entities;

namespace EnergiApp.Infrastructure.Persistence.Mappers
{
    public static class AuctionMapping
    {
        public static Auction ToDomain(this AuctionEntity entity)
        {
            return new Auction
            {
                id = entity.Id.ToString(),
                name = entity.Name,
                state = (Auction.AuctionStateType)entity.State,
                closeForBidding = entity.CloseForBidding,
                deliveryStart = entity.DeliveryStart,
                deliveryEnd = entity.DeliveryEnd,
                contracts = entity.Contracts?.Select(c => c!.ToDomain()).ToList() ?? new List<Contract>(), 
                currencies = entity.Currencies?.Select(c => c!.ToDomain()).ToList() ?? new List<Currency>(),
                portfolios = entity.Portfolios?.Select(p => p!.ToDomain()).ToList() ?? new List<Portfolio>()
            };
        }


        public static AuctionEntity ToEntity(this Auction domain)
        {
            return new AuctionEntity
            {
                Id = Guid.Parse(domain.id),
                Name = domain.name,
                State = (AuctionStateType)domain.state,
                CloseForBidding = domain.closeForBidding,
                DeliveryStart = domain.deliveryStart,
                DeliveryEnd = domain.deliveryEnd,
                Contracts = domain.contracts?.Select(c => c!.ToEntity()).ToList() ?? new List<ContractEntity>(),
                Currencies = domain.currencies?.Select(c => c!.ToEntity()).ToList() ?? new List<CurrencyEntity>(),
                Portfolios = domain.portfolios?.Select(p => p!.ToEntity()).ToList() ?? new List<PortfolioEntity>()
            };
        }

    }

}
