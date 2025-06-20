using MediatR;
using SaleService.Api.Feature.Sales.Abstraction;
using SaleService.Api.Common;
using System.Collections.Generic;

namespace SaleService.Api.Feature.Sales.Query
{
    public class GetAllSalesQuery : IRequest<BaseResponse<IEnumerable<BaseCommandSale>>>
    {
    }
} 