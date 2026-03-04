using EnergiApp.Domain;
using EnergiApp.Infrastructure.Persistence.Entities;

namespace EnergiApp.Infrastructure.Persistence.Mappers
{
    public static class TradeMapping
    {
        public static Trade ToDomain(this TradeEntity entity)
        {
            return new Trade
            {
                ContractId = entity.ContractId,
                TradeId = entity.TradeId,
                DeliveryEnd = entity.DeliveryEnd,
                DeliveryStart = entity.DeliveryStart,
                Volume = entity.Volume,
                Price = entity.Price,
                Side = (TradeSide)entity.Side,
                Status = (AuctionResultState)entity.Status

                
            };
        }


        public static TradeEntity ToEntity(this Trade domain)
        {
            return new TradeEntity
            {
                ContractId = domain.ContractId,
                TradeId = domain.TradeId,
                DeliveryEnd = domain.DeliveryEnd,
                DeliveryStart = domain.DeliveryStart,
                Volume = domain.Volume,
                Price = domain.Price,
                Side = (int)domain.Side,
                Status = domain.Status

            };
        }

    }

}
