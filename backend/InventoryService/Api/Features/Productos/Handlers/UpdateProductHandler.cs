using InventoryService.Api.Common;
using InventoryService.Api.Features.Productos.Commands;
using InventoryService.Domain.Interfaces;
using InventoryService.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace InventoryService.Api.Features.Productos.Handlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, BaseResponse<bool>>
    {
        private readonly IGenericRepository<Product> _repository;

        public UpdateProductHandler(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<BaseResponse<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);
            if (product == null)
            {
                return BaseResponse<bool>.FailureResponse("Producto no encontrado");
            }

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = (double)request.Price;
            product.Category = request.Category;
            product.Stock = request.Stock;
            product.IdSupplier = request.SupplierId;

            await _repository.UpdateAsync(product);
            return BaseResponse<bool>.SuccessResponse(true);
        }
    }
} 