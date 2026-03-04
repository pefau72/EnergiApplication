using EnergiApp.Domain;
using EnergiApp.Infrastructure.Persistence.Entities;

namespace EnergiApp.Infrastructure.Persistence.Mappers
{
    public static class CurveOrderMapping
    {
        public static CurveOrder ToDomain(this CurveOrderEntity entity)
        {
            return new CurveOrder
            {
                OrderId = entity.Id.ToString(),
                AreaCode = entity.AreaCode,
                //State = (OrderStateType)entity.State,
                Portfolio = entity.Portfolio,
                CurrencyCode = entity.CurrencyCode,
                AuctionId = entity.AuctionId.ToString(),
                Modifier = entity.Modifier,
                Modified = entity.Modified,
                Curves = entity.Curves?.Select(c => c!.ToDomain()).ToList() ?? new List<Curve>(), // 1) entity.Curves? er et nulltjek.
                Comment = entity.Comment,                                                         // 2) .Select(c => c!.ToDomain()) konverterer hver CurveEntity til en Curve
                CompanyName = entity.CompanyName,                                                 // 3) .ToList() konverterer resultatet til en List<Curve>.
                Version = entity.Version                                                          // 4) ?? new List<Curve>() håndterer tilfælde, hvor entity.Curves er null, og returnerer en tom liste.
                             



            };
        }


        public static CurveOrderEntity ToEntity(this CurveOrder domain)
        {
            return new CurveOrderEntity
            {
                OrderId = domain.OrderId.ToString(),
                AreaCode = domain.AreaCode,
                // State = (OrderStateType)domain.State,
                Portfolio = domain.Portfolio,
                CurrencyCode = domain.CurrencyCode,
                AuctionId = Guid.Parse(domain.AuctionId),
                Modifier = domain.Modifier,
                Modified = domain.Modified,
                Curves = domain.Curves?.Select(c => c.ToEntity()).ToList() ?? new List<CurveEntity>(),
                Comment = domain.Comment,
                CompanyName = domain.CompanyName,
                Version = domain.Version
            };
        }

    }

}
