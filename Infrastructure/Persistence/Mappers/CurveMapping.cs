using EnergiApp.Domain;
using EnergiApp.Infrastructure.Persistence.Entities;
using EnergiApp.Infrastructure.Persistence.Mappers; // Ensure this is included for CurvePoint mapping

namespace EnergiApp.Infrastructure.Persistence.Mappers
{
    public static class CurveMapping
    {
        public static Curve ToDomain(this CurveEntity entity)
        {
            return new Curve
            {
                ContractId = entity.ContractId,
                CurvePoints = entity.CurvePoints?.Select(p => p!.ToDomain()).ToList() ?? new List<CurvePoint>(), 
            };
        }

        public static CurveEntity ToEntity(this Curve domain)
        {
            return new CurveEntity
            {
                ContractId = domain.ContractId,
                CurvePoints = domain.CurvePoints?.Select(p => p!.ToEntity()).ToList() ?? new List<CurvePointEntity>()
                  

            };
        }
    }
}
