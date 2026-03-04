using EnergiApp.Domain;
using EnergiApp.Infrastructure.Persistence.Entities;

namespace EnergiApp.Infrastructure.Persistence.Mappers
{
    public static class CurvePointMapping
    {
        public static CurvePoint ToDomain(this CurvePointEntity entity)
        {
            return new CurvePoint
            {
                Price = entity.Price,
                Volume = entity.Volume
            };
        }


        public static CurvePointEntity ToEntity(this CurvePoint domain)
        {
            return new CurvePointEntity
            {
                Price = domain.Price,
                Volume = domain.Volume
            };
        }

    }

}
