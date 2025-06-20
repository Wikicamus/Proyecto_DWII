using MediatR;
using SaleService.Api.Common;
using SaleService.Api.Feature.DeliveryStatuFeature;
using SaleService.Api.Feature.DeliveryStatuFeature.Query;
using SaleService.Domain.Models;
using SaleService.Domain.Interfaces;

namespace SaleService.Api.Feature.DeliveryStatuFeature.Handler;

public class GetAllDeliveryStatusHandler : IRequestHandler<GetAllDeliveryStatusQuery, BaseResponse<IEnumerable<DeliveryStatusDTO>>>
{
    private readonly IGenericRepository<DeliveryStatus> _deliveryStatusRepository;

    public GetAllDeliveryStatusHandler(IGenericRepository<DeliveryStatus> deliveryStatusRepository)
    {
        _deliveryStatusRepository = deliveryStatusRepository;
    }

    public async Task<BaseResponse<IEnumerable<DeliveryStatusDTO>>> Handle(GetAllDeliveryStatusQuery request, CancellationToken cancellationToken)
    {
        var deliveryStatuses = await _deliveryStatusRepository.GetAllAsync();
        
        var deliveryStatusDtos = deliveryStatuses.Select(ds => new DeliveryStatusDTO
        {
            Id = ds.Id,
            IdSale = ds.IdSale,
            State = ds.State,
            Description = ds.Description
        });

        return BaseResponse<IEnumerable<DeliveryStatusDTO>>.SuccessResponse(deliveryStatusDtos);
    }
} 