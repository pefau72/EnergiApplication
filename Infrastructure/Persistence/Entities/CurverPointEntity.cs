namespace EnergiApp.Infrastructure.Persistence.Entities;

public class CurvePointEntity
{
    public string Id { get; set; }
    public DateTime Timestamp { get; set; }
    public double Price { get; set; }
    public double Volume { get; set; }

}

