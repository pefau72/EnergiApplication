using EnergiApp.Domain;
using EnergiApp.Infrastructure.Persistence.Entities;
using EnergiApp.Infrastructure.Persistence.Mappers; // This namespace contains the mapping extension methods for converting between domain models and entities.


namespace EnergiApp.Infrastructure.Persistence.Mappers
{
    public static class PortfolioAreaMapping
    {
        public static PortfolioArea ToDomain(this PortfolioAreaEntity entity)
        {
            return new PortfolioArea
                {
                Code = entity.Code,
                Name = entity.Name,
                EicCode = entity.EicCode,
                CurveMaxVolumeLimit = entity.CurveMaxVolumeLimit,
                CurveMinVolumeLimit = entity.CurveMinVolumeLimit

            };

        }
        
        public static PortfolioAreaEntity ToEntity(this PortfolioArea domain)
        {
            return new PortfolioAreaEntity
                {
                Code = domain.Code,
                Name = domain.Name,
                EicCode = domain.EicCode,
                CurveMaxVolumeLimit = domain.CurveMaxVolumeLimit,
                CurveMinVolumeLimit = domain.CurveMinVolumeLimit
            };

        }

    }

}
