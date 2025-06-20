using MediatR;
using SaleService.Api.Common;
using SaleService.Api.Feature.DeliveryStatuFeature;

namespace SaleService.Api.Feature.DeliveryStatuFeature.Query;

public class GetAllDeliveryStatusQuery : IRequest<BaseResponse<IEnumerable<DeliveryStatusDTO>>>
{
} 