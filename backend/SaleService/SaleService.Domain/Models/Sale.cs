using System;
using System.Collections.Generic;

namespace SaleService.Domain.Models;

public partial class Sale
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public int IdClient { get; set; }

    public int IdProduct { get; set; }
    
    public int Units { get; set; }

    public double Total { get; set; }

    public virtual ICollection<DeliveryStatus> DeliveryStatuses { get; set; } = new List<DeliveryStatus>();
}
