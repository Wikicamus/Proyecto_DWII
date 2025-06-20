using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SaleService.Api.Common;
using SaleService.Api.Feature.Sales.Commands;
using SaleService.Domain.Interfaces;
using SaleService.Domain.Models;

namespace SaleService.Api.Feature.Sales.Handlers
{
    public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, BaseResponse<bool>>
    {
        private readonly IGenericRepository<Sale> _repository;

        public DeleteSaleHandler(IGenericRepository<Sale> repository)
        {
            _repository = repository;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var sale = await _repository.GetByIdAsync(request.Id);
                if (sale == null)
                    return BaseResponse<bool>.FailureResponse("No se encontró la venta");

                _repository.Remove(sale);
                return BaseResponse<bool>.SuccessResponse(true);
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.FailureResponse($"Error al eliminar la venta: {ex.Message}");
            }
        }
    }
} 