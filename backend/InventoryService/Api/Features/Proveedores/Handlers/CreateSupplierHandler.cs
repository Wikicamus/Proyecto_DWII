using InventoryService.Api.Common;
using InventoryService.Api.Features.Proveedores.Commands;
using InventoryService.Domain.Interfaces;
using InventoryService.Domain.Models;
using MediatR;

namespace InventoryService.Api.Features.Proveedores.Handlers
{
    public class CreateSupplierHandler : IRequestHandler<CreateSupplierCommand, BaseResponse<int>>
    {
        private readonly IGenericRepository<Supplier> _repository;

        public CreateSupplierHandler(IGenericRepository<Supplier> repository)
        {
            _repository = repository;
        }

        public async Task<BaseResponse<int>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var supplier = new Supplier
                {
                    Name = request.Name,
                    Phone = int.Parse(request.Phone),
                    Address = request.Address
                };

                await _repository.AddAsync(supplier);
                return BaseResponse<int>.SuccessResponse(supplier.Id);
            }
            catch (System.Exception ex)
            {
                return BaseResponse<int>.FailureResponse(ex.Message);
            }
        }
    }
} 