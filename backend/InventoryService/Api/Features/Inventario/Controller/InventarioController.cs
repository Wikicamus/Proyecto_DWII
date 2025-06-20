using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using InventoryService.Api.Common;
using InventoryService.Api.Features.Inventario.Commands;
using InventoryService.Api.Features.Inventario.Queries;
using InventoryService.Api.Features.Inventario.Abstraction;
using InventoryService.Domain.Models;

namespace InventoryService.Api.Features.Inventario.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventarioController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InventarioController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<BaseCommandInventory>>> GetById(int id)
        {
            var query = new GetInventarioByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<int>>> Create([FromBody] CreateInventarioCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<bool>>> Delete(int id)
        {
            var command = new DeleteInventarioCommand { Id = id };
            var result = await _mediator.Send(command);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
    }
} 