using InventoryService.Api.Common;
using InventoryService.Domain.Models;
using InventoryService.Api.Features.Productos.Queries;
using InventoryService.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Api.Features.Productos.Abstraction;

namespace InventoryService.Api.Features.Productos.Handlers
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, BaseResponse<BaseCommandDTO>>
    {
        private readonly IGenericRepository<Product> _repository;

        public GetProductByIdHandler(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<BaseResponse<BaseCommandDTO>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);
            if (product == null)
            {
                return BaseResponse<BaseCommandDTO>.FailureResponse("Producto no encontrado");
            }

            var productDto = new BaseCommandDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = (decimal)product.Price,
                Category = product.Category,
                Stock = product.Stock,
                SupplierId = product.IdSupplier
            };

            return BaseResponse<BaseCommandDTO>.SuccessResponse(productDto);
        }
    }
} 