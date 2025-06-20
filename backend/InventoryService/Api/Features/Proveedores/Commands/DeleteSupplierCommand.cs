using InventoryService.Api.Common;
using MediatR;

namespace InventoryService.Api.Features.Proveedores.Commands
{
    public class DeleteSupplierCommand : IRequest<BaseResponse<bool>>
    {
        public int Id { get; set; }
    }
} 