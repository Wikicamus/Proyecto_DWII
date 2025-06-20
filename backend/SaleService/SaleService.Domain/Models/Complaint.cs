using System.ComponentModel.DataAnnotations.Schema;

namespace SaleService.Domain.Models;

public partial class Complaint
{
    public int Id { get; set; }

    [Column("idsale")]
    public int IdSale { get; set; }

    [Column("reason")]
    public string Reason { get; set; } = null!;

    [Column("description")]
    public string Description { get; set; } = null!;

    public virtual Sale IdSaleNavigation { get; set; } = null!;
} 