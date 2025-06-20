using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SaleService.Api.Common;
using SaleService.Api.Feature.Sales.Query;
using SaleService.Domain.Interfaces;
using SaleService.Domain.Models;
using SaleService.Api.Feature.Sales.Abstraction;

namespace SaleService.Api.Feature.Sales.Handlers
{
    public class GetSaleByIdHandler : IRequestHandler<GetSaleByIdQuery, BaseResponse<BaseCommandSale>>
    {
        private readonly IGenericRepository<Sale> _repository;

        public GetSaleByIdHandler(IGenericRepository<Sale> repository)
        {
            _repository = repository;
        }

        public async Task<BaseResponse<BaseCommandSale>> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var sale = await _repository.GetByIdAsync(request.Id);
                if (sale == null)
                    return BaseResponse<BaseCommandSale>.FailureResponse("No se encontró la venta");

                var response = new BaseCommandSale
                {
                    Id = sale.Id,
                    Date = sale.Date,
                    IdClient = sale.IdClient,
                    IdProduct = sale.IdProduct,
                    Total = sale.Total
                };

                return BaseResponse<BaseCommandSale>.SuccessResponse(response);
            }
            catch (Exception ex)
            {
                return BaseResponse<BaseCommandSale>.FailureResponse($"Error al obtener la venta: {ex.Message}");
            }
        }
    }
} 