using InventoryService.Api.Common;
using Api.Features.Proveedores.Abstraction;
using MediatR;

namespace InventoryService.Api.Features.Proveedores.Queries
{
    public class GetSupplierByIdQuery : IRequest<BaseResponse<BaseResponseSupplier>>
    {
        public int Id { get; set; }
    }
} 