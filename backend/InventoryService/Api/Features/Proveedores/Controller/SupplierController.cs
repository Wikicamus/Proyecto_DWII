using InventoryService.Api.Common;
using InventoryService.Api.Features.Proveedores.Commands;
using InventoryService.Api.Features.Proveedores.Queries;
using Api.Features.Proveedores.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryService.Api.Features.Proveedores.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SupplierController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<int>>> Create([FromBody] CreateSupplierCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<BaseResponseSupplier>>> GetById(int id)
        {
            var query = new GetSupplierByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<IEnumerable<SupplierDto>>>> GetAll()
        {
            var query = new GetAllSuppliersQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<int>>> Update(int id, [FromBody] UpdateSupplierCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<bool>>> Delete(int id)
        {
            var command = new DeleteSupplierCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
} 