namespace EnergiApp.Domain
{

    public class PortfolioVolumesResponse
    {
        public string AuctionId { get; set; }
        public List<PortfolioNetVolume> PortfolioNetVolumes { get; set; }
    }
}