namespace EnergiApp.Domain
{
    public class Portfolio
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Currency { get; set; }
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public PortfolioPermission Permission { get; set; }
        public List<PortfolioArea> Areas { get; set; }
    }
}
