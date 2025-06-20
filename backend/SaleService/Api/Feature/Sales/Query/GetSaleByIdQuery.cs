using MediatR;
using SaleService.Api.Feature.Sales.Abstraction;
using SaleService.Api.Common;

namespace SaleService.Api.Feature.Sales.Query
{
    public class GetSaleByIdQuery : IRequest<BaseResponse<BaseCommandSale>>
    {
        public int Id { get; set; }
    }
} 