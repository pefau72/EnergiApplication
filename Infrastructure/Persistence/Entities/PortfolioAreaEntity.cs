using EnergiApp.Domain;

namespace EnergiApp.Infrastructure.Persistence.Entities;

public class PortfolioAreaEntity
{
    public int Id { get; set; }   // Primary key. Inferred by convention. EF Core will treat this as the primary key of the table.
    public string Code { get; set; }
    public string Name { get; set; }
    public string EicCode { get; set; }
    public decimal CurveMinVolumeLimit { get; set; }
    public decimal CurveMaxVolumeLimit { get; set; }
}


