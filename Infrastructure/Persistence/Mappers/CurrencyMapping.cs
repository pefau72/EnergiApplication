using EnergiApp.Domain;
using EnergiApp.Infrastructure.Persistence.Entities;
using EnergiApp.Infrastructure.Persistence.Mappers; // This namespace contains the mapping extension methods for converting between domain models and entities.


namespace EnergiApp.Infrastructure.Persistence.Mappers
{
    public static class CurrencyMapping
    {
        public static Currency ToDomain(this CurrencyEntity entity)
        {
            return new Currency
            {
                CurrencyCode = entity.CurrencyCode,
                MinPrice = entity.MinPrice,
                MaxPrice = entity.MaxPrice                
            };
        }
        
        public static CurrencyEntity ToEntity(this Currency domain)
        {
            return new CurrencyEntity
            {
                CurrencyCode = domain.CurrencyCode,
                MinPrice = domain.MinPrice,
                MaxPrice = domain.MaxPrice,
            };
        }

    }

}
