namespace EnergiApp.Domain
{
    public class AreaContractGroup
    {
        public string AreaCode { get; set; }
        public IEnumerable<Contract> Contracts { get; set; }
    }
}