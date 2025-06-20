using MediatR;
using SaleService.Api.Common;
using SaleService.Api.Feature.DeliveryStatuFeature.Command;
using SaleService.Domain.Models;
using SaleService.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaleService.Api.Feature.DeliveryStatuFeature.Handler;

public class CreateDeliveryStatusHandler : IRequestHandler<CreateDeliveryStatusCommand, BaseResponse<int>>
{
    private readonly IGenericRepository<DeliveryStatus> _repository;
    public CreateDeliveryStatusHandler(IGenericRepository<DeliveryStatus> repository)
    {
        _repository = repository;
    }

    public async Task<BaseResponse<int>> Handle(CreateDeliveryStatusCommand request, CancellationToken cancellationToken)
    {
        var deliveryStatus = new DeliveryStatus
        {
            IdSale = request.IdSale,
            State = request.State,
            Description = request.Description
        };
        await _repository.AddAsync(deliveryStatus);
        return BaseResponse<int>.SuccessResponse(deliveryStatus.Id);
    }
} 