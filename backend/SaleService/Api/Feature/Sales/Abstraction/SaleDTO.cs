using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace SaleService.Api.Feature.Sales.Abstraction
{
    public class SaleDTO
    {
        public DateTime Date { get; set; }

        public int IdClient { get; set; }

        public int IdProduct { get; set; }

        public int Units { get; set; }

        [JsonIgnore]
        public double Total { get; set; }
    }
}