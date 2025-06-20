namespace SaleService.Api.Feature.Complaints;

public class ComplaintDTO
{
    public int Id { get; set; }
    public int IdSale { get; set; }
    public string Reason { get; set; } = null!;
    public string Description { get; set; } = null!;
} 