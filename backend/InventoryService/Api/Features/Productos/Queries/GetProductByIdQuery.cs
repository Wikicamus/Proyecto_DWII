using InventoryService.Api.Common;
using Api.Features.Productos.Abstraction;
using MediatR;

namespace InventoryService.Api.Features.Productos.Queries
{
    public class GetProductByIdQuery : IRequest<BaseResponse<BaseCommandDTO>>
    {
        
        public int Id { get; set; }
    }
} 