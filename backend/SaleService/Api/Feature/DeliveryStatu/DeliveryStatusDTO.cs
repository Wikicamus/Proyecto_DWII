namespace SaleService.Api.Feature.DeliveryStatuFeature;

public class DeliveryStatusDTO
{
    public int Id { get; set; }
    public int IdSale { get; set; }
    public string State { get; set; } = null!;
    public string Description { get; set; } = null!;
} 