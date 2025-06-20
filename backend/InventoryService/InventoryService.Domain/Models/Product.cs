using System;
using System.Collections.Generic;

namespace InventoryService.Domain.Models;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public double Price { get; set; }

    public string Category { get; set; } = null!;

    public int Stock { get; set; }

    public int IdSupplier { get; set; }


    public virtual Supplier Supplier { get; set; } = null!;
}
