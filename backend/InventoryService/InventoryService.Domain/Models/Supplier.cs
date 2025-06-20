using System;

namespace InventoryService.Domain.Models;

public partial class Supplier
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Phone { get; set; }

    public string Address { get; set; } = null!;

}
