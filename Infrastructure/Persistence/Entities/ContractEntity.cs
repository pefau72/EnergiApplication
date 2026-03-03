using System;
using System.Collections.Generic;
using System.Text;

namespace EnergiApp.Infrastructure.Persistence.Entities
{
    public class ContractEntity
    {
        public string Id { get; set; }
        public DateTime DeliveryStart { get; set; }
        public DateTime DeliveryEnd { get; set; }
    }
}
