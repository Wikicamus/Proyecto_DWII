using InventoryService.Api.Common;
using Api.Features.Proveedores.Abstraction;
using MediatR;

namespace InventoryService.Api.Features.Proveedores.Queries
{
    public class GetAllSuppliersQuery : IRequest<BaseResponse<IEnumerable<SupplierDto>>>
    {
    }
} 