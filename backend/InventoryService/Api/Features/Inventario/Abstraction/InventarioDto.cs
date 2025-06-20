using System;

namespace InventoryService.Api.Features.Inventario.Abstraction
{
    public class InventarioDto
    {
        public int IdProduct { get; set; }

        public int IdEmployee { get; set; }

        public string MovementType { get; set; } = null!;

        public int Quantity { get; set; }

        public DateTime MovementDate { get; set; }

    }
}