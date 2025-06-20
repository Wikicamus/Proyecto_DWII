using InventoryService.Api.Common;
using Api.Features.Productos.Abstraction;
using MediatR;
using System.Collections.Generic;

namespace InventoryService.Api.Features.Productos.Queries
{
    public class GetAllProductsQuery : IRequest<BaseResponse<IEnumerable<BaseCommandDTO>>>
    {
    }
} 