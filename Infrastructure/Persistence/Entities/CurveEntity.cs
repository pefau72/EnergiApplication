using EnergiApp.Domain;
using EnergiApp.Infrastructure.Persistence.Entities;

namespace EnergiApp.Infrastructure.Persistence.Entities
{
    public class CurveEntity
    {
        public string Id { get; set; }
        public string ContractId { get; set; }
        public List<CurvePointEntity> CurvePoints { get; set; }
    }
}
