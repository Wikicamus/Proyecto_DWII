using InventoryService.Api.Common;
using Api.Features.Productos.Abstraction;
using MediatR;
using System.Text.Json.Serialization;

namespace InventoryService.Api.Features.Productos.Commands
{
    public class UpdateProductCommand : BaseCommandDTO, IRequest<BaseResponse<bool>>
    {
    }
} 