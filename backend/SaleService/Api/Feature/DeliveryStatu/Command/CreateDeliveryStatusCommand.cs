using MediatR;
using SaleService.Api.Common;

namespace SaleService.Api.Feature.DeliveryStatuFeature.Command;

public class CreateDeliveryStatusCommand : IRequest<BaseResponse<int>>
{
    public int IdSale { get; set; }
    public string State { get; set; } = null!;
    public string Description { get; set; } = null!;
} 