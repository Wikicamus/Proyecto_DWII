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
    public class DeleteInventarioHandler : IRequestHandler<DeleteInventarioCommand, BaseResponse<bool>>
    {
        private readonly IGenericRepository<Inventory> _repository;

        public DeleteInventarioHandler(IGenericRepository<Inventory> repository)
        {
            _repository = repository;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteInventarioCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var inventario = await _repository.GetByIdAsync(request.Id);
                if (inventario == null)
                    return BaseResponse<bool>.FailureResponse("No se encontró el inventario");

                await _repository.DeleteAsync(inventario);
                return BaseResponse<bool>.SuccessResponse(true);
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse(ex.Message);
            }
        }
    }
} 