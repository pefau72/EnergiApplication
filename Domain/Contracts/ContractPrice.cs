
namespace EnergiApp.Domain
{      
    public class ContractPrice
    {
        public string ContractId { get; set; }
        public DateTime DeliveryStart { get; set; }
        public DateTime DeliveryEnd { get; set; }
        public List<AreaPrice> Areas { get; set; }
    }
}