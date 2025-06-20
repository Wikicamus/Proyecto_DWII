using System;
using System.Collections.Generic;
using System.Linq;
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
    public class GetAllSalesHandler : IRequestHandler<GetAllSalesQuery, BaseResponse<IEnumerable<BaseCommandSale>>>
    {
        private readonly IGenericRepository<Sale> _repository;

        public GetAllSalesHandler(IGenericRepository<Sale> repository)
        {
            _repository = repository;
        }

        public async Task<BaseResponse<IEnumerable<BaseCommandSale>>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var sales = await _repository.GetAllAsync();
                var response = sales.Select(sale => new BaseCommandSale
                {
                    Id = sale.Id,
                    Date = sale.Date,
                    IdClient = sale.IdClient,
                    IdProduct = sale.IdProduct,
                    Total = sale.Total
                });
                return BaseResponse<IEnumerable<BaseCommandSale>>.SuccessResponse(response);
            }
            catch (Exception ex)
            {
                return BaseResponse<IEnumerable<BaseCommandSale>>.FailureResponse($"Error al obtener las ventas: {ex.Message}");
            }
        }
    }
} 