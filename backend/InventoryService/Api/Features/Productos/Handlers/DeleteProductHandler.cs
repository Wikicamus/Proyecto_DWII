using InventoryService.Api.Common;
using InventoryService.Api.Features.Productos.Commands;
using InventoryService.Domain.Interfaces;
using InventoryService.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace InventoryService.Api.Features.Productos.Handlers
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, BaseResponse<bool>>
    {
        private readonly IGenericRepository<Product> _productRepository;

        public DeleteProductHandler(IGenericRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);
            if (product == null)
            {
                return BaseResponse<bool>.FailureResponse("Producto no encontrado");
            }

            await _productRepository.DeleteAsync(product);

            return BaseResponse<bool>.SuccessResponse(true, "Producto eliminado exitosamente");
        }
    }
} 