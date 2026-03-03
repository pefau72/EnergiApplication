namespace EnergiApp.Domain
{
    
    public class PortfolioNetVolume
    {
        public string Portfolio { get; set; }
        public string CompanyName { get; set; }
        public List<AreaNetVolume> AreaNetVolumes { get; set; }
    }
}