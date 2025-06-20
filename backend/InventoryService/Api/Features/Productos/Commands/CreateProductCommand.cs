using InventoryService.Api.Common;
using Api.Features.Productos.Abstraction;
using MediatR;

namespace InventoryService.Api.Features.Productos.Commands
{
    public class CreateProductCommand : ProductDto, IRequest<BaseResponse<int>>
    {
    }
} 