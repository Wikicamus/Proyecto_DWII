using InventoryService.Api.Common;
using MediatR;

namespace InventoryService.Api.Features.Productos.Commands
{
    public class DeleteProductCommand : IRequest<BaseResponse<bool>>
    {
        public int Id { get; set; }
    }
} 