using Microsoft.AspNetCore.Mvc;
using MediatR;
using SaleService.Api.Feature.Sales.Commands;
using SaleService.Api.Feature.Sales.Query;
using SaleService.Api.Common;

namespace SaleService.Api.Feature.Sales.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SalesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllSalesQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetSaleByIdQuery { Id = id });
            if (result.Data == null)
                return NotFound(result.Message);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSaleCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Data }, result);
        }

    }
}