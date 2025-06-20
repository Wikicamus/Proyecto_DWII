using MediatR;
using InventoryService.Api.Common;
using InventoryService.Api.Features.Inventario.Abstraction;

namespace InventoryService.Api.Features.Inventario.Queries
{
    public class GetInventarioByIdQuery : IRequest<BaseResponse<BaseCommandInventory>>
    {
        public int Id { get; set; }
    }
} 