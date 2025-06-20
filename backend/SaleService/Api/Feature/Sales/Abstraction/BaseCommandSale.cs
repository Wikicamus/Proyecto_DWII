using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleService.Api.Feature.Sales.Abstraction
{
    public class BaseCommandSale : SaleDTO
    {
        public int Id { get; set; }
    }
}