namespace EnergiApp.Domain
{
    public class AreaPrice
    {
        public string AreaCode { get; set; }
        public List<CurrencyPrice> Prices { get; set; }
    }
}