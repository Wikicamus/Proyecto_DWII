using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryService.Api.Features.Inventario.Abstraction;

namespace InventoryService.Api.Features.Inventario.Abstraction
{
    public class BaseCommandInventory : InventarioDto
    {
        public int Id { get; set; }
    }
}