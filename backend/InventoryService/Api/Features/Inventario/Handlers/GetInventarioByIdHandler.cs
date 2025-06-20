using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using InventoryService.Api.Common;
using InventoryService.Api.Features.Inventario.Queries;
using InventoryService.Domain.Interfaces;
using InventoryService.Domain.Models;
using InventoryService.Api.Features.Inventario.Abstraction;

namespace InventoryService.Api.Features.Inventario.Handlers
{
    public class GetInventarioByIdHandler : IRequestHandler<GetInventarioByIdQuery, BaseResponse<BaseCommandInventory>>
    {
        private readonly IGenericRepository<Inventory> _repository;

        public GetInventarioByIdHandler(IGenericRepository<Inventory> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<BaseResponse<BaseCommandInventory>> Handle(GetInventarioByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var inventario = await _repository.GetByIdAsync(request.Id);
                if (inventario == null)
                    return BaseResponse<BaseCommandInventory>.FailureResponse("No se encontró el inventario");

                var response = new BaseCommandInventory
                {
                    Id = inventario.Id,
                    IdProduct = inventario.IdProduct,
                    IdEmployee = inventario.IdEmployee,
                    MovementType = inventario.MovementType,
                    Quantity = inventario.Quantity,
                    MovementDate = inventario.MovementDate
                };

                return BaseResponse<BaseCommandInventory>.SuccessResponse(response);
            }
            catch (Exception ex)
            {
                return BaseResponse<BaseCommandInventory>.FailureResponse($"Error al obtener el inventario: {ex.Message}");
            }
        }
    }
}