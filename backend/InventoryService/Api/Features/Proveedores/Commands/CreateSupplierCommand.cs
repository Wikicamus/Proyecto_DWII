using Api.Features.Proveedores.Abstraction;
using InventoryService.Api.Common;
using MediatR;

namespace InventoryService.Api.Features.Proveedores.Commands
{
    public class CreateSupplierCommand : SupplierDto, IRequest<BaseResponse<int>>
    {
    }
} 