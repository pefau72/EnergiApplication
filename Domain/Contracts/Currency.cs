namespace EnergiApp.Domain 
{
    public class Currency
    {
        public string CurrencyCode { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
    }
}
