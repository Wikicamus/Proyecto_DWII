using System;

namespace InventoryService.Domain.Models;

public partial class Inventory
{
    public int Id { get; set; }

    public int IdProduct { get; set; }

    public int IdEmployee { get; set; }

    public string MovementType { get; set; } = null!;

    public int Quantity { get; set; }

    public DateTime MovementDate { get; set; }

    public virtual Product Product { get; set; } = null!;
}
