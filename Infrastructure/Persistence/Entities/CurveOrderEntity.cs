namespace EnergiApp.Infrastructure.Persistence.Entities;

public class CurveOrderEntity
{
    public Guid Id { get; set; }
    public string OrderId { get; set; }
    public string AreaCode { get; set; }
    public string Portfolio { get; set; }
    public string CurrencyCode { get; set; }
    public Guid AuctionId { get; set; }
    public string Modifier { get; set; }
    public DateTime Modified { get; set; }
    public string State { get; set; }
    public List<CurveEntity> Curves { get; set; }
    public string Comment { get; set; }
    public string CompanyName { get; set; }
    public int Version { get; set; }
}

