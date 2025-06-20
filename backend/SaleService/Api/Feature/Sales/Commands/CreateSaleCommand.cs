using MediatR;
using SaleService.Api.Feature.Sales.Abstraction;
using SaleService.Api.Common;

namespace SaleService.Api.Feature.Sales.Commands
{
    public class CreateSaleCommand : SaleDTO, IRequest<BaseResponse<int>>
    {
    }
} 