using MediatR;
using Microsoft.AspNetCore.Mvc;
using SaleService.Api.Common;
using SaleService.Api.Feature.Complaints.Command;
using SaleService.Api.Feature.Complaints.Query;

namespace SaleService.Api.Feature.Complaints.Controller;

[ApiController]
[Route("api/[controller]")]
public class ComplaintsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ComplaintsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse<int>>> CreateComplaint([FromBody] CreateComplaintCommand command)
    {
        var response = await _mediator.Send(command);
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponse<ComplaintDTO>>> GetComplaintById(int id)
    {
        var query = new GetComplaintByIdQuery { Id = id };
        var response = await _mediator.Send(query);
        
        if (!response.Success)
        {
            return NotFound(response);
        }
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<IEnumerable<ComplaintDTO>>>> GetAllComplaints()
    {
        var query = new GetAllComplaintsQuery();
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BaseResponse<bool>>> UpdateComplaint(int id, [FromBody] UpdateComplaintCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(BaseResponse<bool>.FailureResponse("El ID de la URL no coincide con el ID del comando."));
        }

        var response = await _mediator.Send(command);
        if (!response.Success)
        {
            return NotFound(response);
        }
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<BaseResponse<bool>>> DeleteComplaint(int id)
    {
        var command = new DeleteComplaintCommand { Id = id };
        var response = await _mediator.Send(command);
        
        if (!response.Success)
        {
            return NotFound(response);
        }
        return Ok(response);
    }
} 