using EnergiApp.Domain;
using EnergiApp.Infrastructure.Persistence.Entities;
using EnergiApp.Infrastructure.Persistence.Mappers; // This namespace contains the mapping extension methods for converting between domain models and entities.


namespace EnergiApp.Infrastructure.Persistence.Mappers
{
    public static class ContractsMapping
    {
        public static Contract ToDomain(this ContractEntity entity)
        {
            return new Contract
            {
                Id = entity.Id.ToString(),
                DeliveryStart = entity.DeliveryStart,
                DeliveryEnd = entity.DeliveryEnd                
            };
        }
        
        public static ContractEntity ToEntity(this Contract domain)
        {
            return new ContractEntity
            {
                Id = domain.Id.ToString(),
                DeliveryStart = domain.DeliveryStart,
                DeliveryEnd = domain.DeliveryEnd,
            };
        }

    }

}
