using EnergiApp.Domain;

namespace EnergiApp.Infrastructure.Persistence.Entities
{
    public class CurveEntity
    {
        public string ContractId { get; set; }
        public List<CurvePoint> CurvePoints { get; set; }
    }
}
