using MediatR;
using Microsoft.AspNetCore.Mvc;
using SaleService.Api.Common;
using SaleService.Api.Feature.DeliveryStatuFeature.Command;
using SaleService.Api.Feature.DeliveryStatuFeature.Query;

namespace SaleService.Api.Feature.DeliveryStatuFeature.Controller;

[ApiController]
[Route("api/[controller]")]
public class DeliveryStatusController : ControllerBase
{
    private readonly IMediator _mediator;

    public DeliveryStatusController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<IEnumerable<DeliveryStatusDTO>>>> GetAllDeliveryStatus()
    {
        var query = new GetAllDeliveryStatusQuery();
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse<int>>> Create([FromBody] CreateDeliveryStatusCommand command)
        => await _mediator.Send(command);
} 