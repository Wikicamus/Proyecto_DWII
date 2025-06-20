using InventoryService.Api.Common;
using InventoryService.Api.Features.Proveedores.Commands;
using InventoryService.Domain.Interfaces;
using InventoryService.Domain.Models;
using MediatR;

namespace InventoryService.Api.Features.Proveedores.Handlers
{
    public class UpdateSupplierHandler : IRequestHandler<UpdateSupplierCommand, BaseResponse<int>>
    {
        private readonly IGenericRepository<Supplier> _repository;

        public UpdateSupplierHandler(IGenericRepository<Supplier> repository)
        {
            _repository = repository;
        }

        public async Task<BaseResponse<int>> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            using var transaction = await _repository.BeginTransactionAsync();
            try
            {
                var supplier = await _repository.GetByIdAsync(request.Id);
                if (supplier == null)
                {
                    return BaseResponse<int>.FailureResponse("Proveedor no encontrado");
                }

                supplier.Name = request.Name;
                supplier.Phone = int.Parse(request.Phone);
                supplier.Address = request.Address;

                await _repository.UpdateAsync(supplier);
                await transaction.CommitAsync();

                return BaseResponse<int>.SuccessResponse(200, "Proveedor actualizado correctamente");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BaseResponse<int>.FailureResponse($"Error al actualizar el proveedor: {ex.Message}");
            }
        }
    }
} 