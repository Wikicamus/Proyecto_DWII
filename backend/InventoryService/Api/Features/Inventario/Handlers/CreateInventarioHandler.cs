using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using InventoryService.Api.Common;
using InventoryService.Api.Features.Inventario.Commands;
using InventoryService.Domain.Interfaces;
using InventoryService.Domain.Models;

namespace InventoryService.Api.Features.Inventario.Handlers
{
    public class CreateInventarioHandler : IRequestHandler<CreateInventarioCommand, BaseResponse<int>>
    {
        private readonly IGenericRepository<Inventory> _repository;
        private readonly IGenericRepository<Product> _productRepository;

        public CreateInventarioHandler(
            IGenericRepository<Inventory> repository,
            IGenericRepository<Product> productRepository)
        {
            _repository = repository;
            _productRepository = productRepository;
        }

        public async Task<BaseResponse<int>> Handle(CreateInventarioCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.MovementType) || 
                    (request.MovementType != "IN" && request.MovementType != "OUT"))
                {
                    return BaseResponse<int>.FailureResponse("El tipo de movimiento debe ser 'IN' u 'OUT'");
                }

                // Validar que el producto existe
                var product = await _productRepository.GetByIdAsync(request.IdProduct);
                if (product == null)
                {
                    return BaseResponse<int>.FailureResponse("El producto especificado no existe");
                }

                var inventario = new Inventory
                {
                    IdProduct = request.IdProduct,
                    IdEmployee = request.IdEmployee,
                    MovementType = request.MovementType,
                    Quantity = request.Quantity,
                    MovementDate = request.MovementDate
                };

                await _repository.AddAsync(inventario);
                return BaseResponse<int>.SuccessResponse(inventario.Id);
            }
            catch (Exception ex)
            {
                return BaseResponse<int>.FailureResponse(ex.Message);
            }
        }
    }
} 