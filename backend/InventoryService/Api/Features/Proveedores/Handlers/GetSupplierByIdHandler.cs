using InventoryService.Api.Common;
using InventoryService.Domain.Models;
using InventoryService.Api.Features.Proveedores.Queries;
using InventoryService.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Api.Features.Proveedores.Abstraction;

namespace InventoryService.Api.Features.Proveedores.Handlers
{
    public class GetSupplierByIdHandler : IRequestHandler<GetSupplierByIdQuery, BaseResponse<BaseResponseSupplier>>
    {
        private readonly IGenericRepository<Supplier> _repository;

        public GetSupplierByIdHandler(IGenericRepository<Supplier> repository)
        {
            _repository = repository;
        }

        public async Task<BaseResponse<BaseResponseSupplier>> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
        {
            var supplier = await _repository.GetByIdAsync(request.Id);
            if (supplier == null)
            {
                return BaseResponse<BaseResponseSupplier>.FailureResponse("Proveedor no encontrado");
            }

            var supplierDto = new BaseResponseSupplier
            {
                Name = supplier.Name,
                Phone = supplier.Phone.ToString(),
                Address = supplier.Address
            };

            return BaseResponse<BaseResponseSupplier>.SuccessResponse(supplierDto);
        }
    }
} 