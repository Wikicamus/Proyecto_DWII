using MediatR;
using SaleService.Api.Common;

namespace SaleService.Api.Feature.Sales.Commands
{
    public class DeleteSaleCommand : IRequest<BaseResponse<bool>>
    {
        public int Id { get; set; }
    }
} 