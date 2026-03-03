using EnergiApp.Domain;
using EnergiApp.Infrastructure.Persistence.Entities;
using EnergiApp.Infrastructure.Persistence.Mappers; // This namespace contains the mapping extension methods for converting between domain models and entities.


namespace EnergiApp.Infrastructure.Persistence.Mappers
{
    public static class PortfolioMapping
    {
        public static Portfolio ToDomain(this PortfolioEntity entity)
        {
            return new Portfolio
                {
                Name = entity.Name,
                Id = entity.Id,
                Currency = entity.Currency,
                CompanyId = entity.CompanyId,
                CompanyName = entity.CompanyName,
                Permission = entity.Permission,
                Areas = entity.Areas
            };

        }
        
        public static PortfolioEntity ToEntity(this Portfolio domain)
        {
            return new PortfolioEntity
                {
                Name = domain.Name,
                Id = domain.Id,
                Currency = domain.Currency,
                CompanyId = domain.CompanyId,
                CompanyName = domain.CompanyName,
                Permission = domain.Permission,
                Areas = domain.Areas
            };

        }

    }

}
