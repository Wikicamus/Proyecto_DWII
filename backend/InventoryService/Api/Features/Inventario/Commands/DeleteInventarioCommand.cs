using MediatR;
using InventoryService.Api.Common;

namespace InventoryService.Api.Features.Inventario.Commands
{
    public class DeleteInventarioCommand : IRequest<BaseResponse<bool>>
    {
        public int Id { get; set; }
    }
} 