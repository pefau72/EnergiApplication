namespace EnergiApp.Infrastructure.Persistence.Entities;

public class TradeEntity
{
    public Guid Id { get; set; }
    public string TradeId { get; set; }
    public string ContractId { get; set; }
    public DateTime DeliveryStart { get; set; }
    public DateTime DeliveryEnd { get; set; }
    public decimal Volume { get; set; }
    public decimal Price { get; set; }
    public int Side { get; set; }
    public string Status { get; set; }
}


