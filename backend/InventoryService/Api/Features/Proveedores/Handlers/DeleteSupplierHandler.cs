using InventoryService.Api.Common;
using InventoryService.Api.Features.Proveedores.Commands;
using InventoryService.Domain.Interfaces;
using InventoryService.Domain.Models;
using MediatR;

namespace InventoryService.Api.Features.Proveedores.Handlers
{
    public class DeleteSupplierHandler : IRequestHandler<DeleteSupplierCommand, BaseResponse<bool>>
    {
        private readonly IGenericRepository<Supplier> _repository;

        public DeleteSupplierHandler(IGenericRepository<Supplier> repository)
        {
            _repository = repository;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = await _repository.GetByIdAsync(request.Id);
            if (supplier == null)
            {
                return BaseResponse<bool>.FailureResponse("Proveedor no encontrado");
            }

            await _repository.DeleteAsync(supplier);
            return BaseResponse<bool>.SuccessResponse(true);
        }
    }
} 