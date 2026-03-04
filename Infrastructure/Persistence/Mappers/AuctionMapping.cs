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
                Id = entity.Id.ToString(),
                Name = entity.Name,
                State = (Auction.AuctionStateType)entity.State,
                CloseForBidding = entity.CloseForBidding,
                DeliveryStart = entity.DeliveryStart,
                DeliveryEnd = entity.DeliveryEnd,
                Contracts = entity.Contracts?.Select(c => c!.ToDomain()).ToList() ?? new List<Contract>(), 
                Currencies = entity.Currencies?.Select(c => c!.ToDomain()).ToList() ?? new List<Currency>(),
                Portfolios = entity.Portfolios?.Select(p => p!.ToDomain()).ToList() ?? new List<Portfolio>()
            };
        }


        public static AuctionEntity ToEntity(this Auction domain)
        {
            return new AuctionEntity
            {
                Id = Guid.Parse(domain.Id),
                Name = domain.Name,
                State = (AuctionStateType)domain.State,
                CloseForBidding = domain.CloseForBidding,
                DeliveryStart = domain.DeliveryStart,
                DeliveryEnd = domain.DeliveryEnd,
                Contracts = domain.Contracts?.Select(c => c!.ToEntity()).ToList() ?? new List<ContractEntity>(),
                Currencies = domain.Currencies?.Select(c => c!.ToEntity()).ToList() ?? new List<CurrencyEntity>(),
                Portfolios = domain.Portfolios?.Select(p => p!.ToEntity()).ToList() ?? new List<PortfolioEntity>()
            };
        }

    }

}
