using System.ComponentModel.DataAnnotations.Schema;

namespace SaleService.Domain.Models;

public partial class DeliveryStatus
{
    public int Id { get; set; }

    [Column("idsale")]
    public int IdSale { get; set; }

    [Column("state")]
    public string State { get; set; } = null!;

    [Column("description")]
    public string Description { get; set; } = null!;

    public virtual Sale IdSaleNavigation { get; set; } = null!;
} 