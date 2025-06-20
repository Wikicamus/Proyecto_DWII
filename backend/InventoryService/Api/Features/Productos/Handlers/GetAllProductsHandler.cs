using InventoryService.Api.Common;
using InventoryService.Domain.Models;
using InventoryService.Api.Features.Productos.Queries;
using Api.Features.Productos.Abstraction;
using MediatR;
using InventoryService.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InventoryService.Api.Features.Productos.Handlers
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, BaseResponse<IEnumerable<BaseCommandDTO>>>
    {
        private readonly IGenericRepository<Product> _repository;

        public GetAllProductsHandler(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<BaseResponse<IEnumerable<BaseCommandDTO>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetAllAsync();
            var productDtos = products.Select(p => new BaseCommandDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = (decimal)p.Price,
                Category = p.Category,
                Stock = p.Stock,
                SupplierId = p.IdSupplier
            });

            return BaseResponse<IEnumerable<BaseCommandDTO>>.SuccessResponse(productDtos);
        }
    }
} 