using System;
using System.Collections.Generic;
using System.Text;

namespace EnergiApp.Infrastructure.Persistence.Entities
{
    public class CurrencyEntity
    {
        public string CurrencyCode { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
    }
}
