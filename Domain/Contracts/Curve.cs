namespace EnergiApp.Domain
{

    public class Curve
    {
        public string ContractId { get; set; }
        public List<CurvePoint> CurvePoints { get; set; }
    }
}